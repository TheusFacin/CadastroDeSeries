using System;

namespace CadastroSeries
{
    public class Serie : EntidadeBase
    {
        private Genero Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private int Ano { get; set; }
        private bool Excluido { get; set; }

        public Serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += $"{this.Titulo}";

            if (this.Excluido) retorno += " (Excluído)";

            retorno += Environment.NewLine;
            retorno += $"- Gênero: {this.Genero}{Environment.NewLine}";
            retorno += $"- Descrição: {this.Descricao}{Environment.NewLine}";
            retorno += $"- Ano de Lançamento: {this.Ano}";

            return retorno;
        }

        public void Excluir()
        {
            this.Excluido = true;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public int retornaId()
        {
            return this.Id;
        }

        public bool foiExcluido()
        {
            return this.Excluido;
        }
    }
}