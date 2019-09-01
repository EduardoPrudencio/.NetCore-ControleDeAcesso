using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Security.Business.Interfaces;
using Security.Business.Notificacao;
using System.Linq;

namespace Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new { Sucesso = true, Data = result });
            }
            return BadRequest(new { Sucess = false, errors = _notificador.ObterNotificacoes().Select(m => m.Mensagem) });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = ModelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var erroMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMessage);
            }
        }

        protected void NotificarErro(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }
    }
}
