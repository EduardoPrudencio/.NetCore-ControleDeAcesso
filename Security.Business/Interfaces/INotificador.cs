using System.Collections.Generic;

namespace Security.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao.Notificacao> ObterNotificacoes();
        void Handle(Notificacao.Notificacao notificacao);
    }
}
