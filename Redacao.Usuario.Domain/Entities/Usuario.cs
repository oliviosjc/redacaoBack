using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Usuario()
        {

        }

        public bool Ativo { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string Telefone { get; private set; }

        public string CPF { get; private set; }

        public DateTime? DataNascimento { get; private set; }

        public string Genero { get; private set; }

        public Guid TipoUsuarioId { get; private set; }

        public TipoUsuario TipoUsuario { get; private set; }

        public Guid ComoConheceuId { get; private set; }

        public ComoConheceu ComoConheceu { get; private set; }

        public ICollection<Atividade> Atividades { get; private set; }

        public Usuario(string nome, string email, string telefone, string cpf, DateTime? dataNascimento, string genero, Guid tipoUsuarioId, Guid comoConheceuId)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Genero = genero;
            TipoUsuarioId = tipoUsuarioId;
            ComoConheceuId = comoConheceuId;

            Validar();
        }

        public void Desativar() => Ativo = false;

        public void Ativar() => Ativo = true;

        public void AlterarNome(string nome)
        {
            Validacoes.ValidarSeVazio(nome, "O nome não pode ser alterado para vazio.");
            Nome = nome;
        }

        public void AlterarEmail(string email)
        {
            Validacoes.ValidarSeVazio(email, "O email não pode ser alterado para vazio");
            Email = email;
        }

        public void AlterarTelefone(string telefone)
        {
            Telefone = telefone;
        }

        public void AlterarDataNascimento(DateTime? dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public void AlterarGenero(string genero)
        {
            Genero = genero;
        }

        public void AlterarTipoUsuario(Guid tipoUsuarioId)
        {
            Validacoes.ValidarSeNulo(tipoUsuarioId, "O campo tipo de usuário não pode ser alterado para nulo.");
            TipoUsuarioId = tipoUsuarioId;
        }

        public void AlterarComoConheceu(Guid comoConheceuId)
        {
            Validacoes.ValidarSeNulo(comoConheceuId, "O campo como conheceu do usuário não pode ser alterado para nulo.");
            ComoConheceuId = comoConheceuId;
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo nome do usuário não pode estar vazio.");
            Validacoes.ValidarTamanho(Nome, 50, "O campo nome do usuário deve ter tamanho máximo de 50 caracteres.");
            Validacoes.ValidarSeVazio(Email, "O campo email do usuário não pode estar vazio.");
            Validacoes.ValidarTamanho(Email, 50, "O campo email do usuário deve ter tamanho máximo de 50 caracteres");
            Validacoes.ValidarSeVazio(Telefone, "O campo telefone do usuário não pode estar vazio.");
            Validacoes.ValidarTamanho(Telefone, 11, "O campo telefone do usuário deve ter tamanho máximo de 50 caracteres");
            Validacoes.ValidarSeNulo(TipoUsuarioId, "O campo tipo do usuário não pode ser nulo");
            Validacoes.ValidarSeNulo(ComoConheceuId, "O campo como conheceu do usuário não pode ser nulo");
        }
    }
}
