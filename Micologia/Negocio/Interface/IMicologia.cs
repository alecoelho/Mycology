using System;
using System.Collections.Generic;
namespace Micologia.Negocio
{
    public interface IMicologia<T>
    {
        void Inserir(T entity);
        void Alterar(T entity);
        void Excluir(T entity);
        IList<T> ListarTodos();
        T ListarPorId(object id);
    }
}
