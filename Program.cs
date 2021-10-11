using System;

namespace CadastroSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Bem vindo(a) :)");
            Console.WriteLine();
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario != "X")
            {
                Console.Clear();

                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Console.WriteLine();
                Console.WriteLine("Pressione <enter> para continuar");
                Console.ReadLine();

                Console.Clear();
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.Clear();
            Console.WriteLine("Até a próxima :)");
            Console.WriteLine("Pressione <enter> para encerrar");
            Console.ReadLine();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine();
            Console.WriteLine("1 - Listar as séries cadastradas");
            Console.WriteLine("2 - Adicionar uma nova série");
            Console.WriteLine("3 - Atualizar uma série");
            Console.WriteLine("4 - Excluir uma série");
            Console.WriteLine("5 - Visualizar uma série");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            Console.Write("> ");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static int ListarSeries()
        {
            Console.WriteLine("Séries Cadastradas:");
            Console.WriteLine();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Não há séries cadastradas");
                return 0;
            }

            foreach (var serie in lista)
            {
                if (serie.foiExcluido()) continue;

                Console.WriteLine($"# {serie.retornaId()}: {serie.retornaTitulo()}");
            }

            return lista.Count;
        }

        private static void InserirSerie()
        {
            Serie novaSerie = ObterEntradas(repositorio.ProximoId());

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            int contagem = ListarSeries();

            if (contagem == 0) return;

            Console.WriteLine();
            int indiceSerie = ObterEntradaNumerica("Insira o id da série a ser atualizada: ", 0, contagem - 1);
            Console.WriteLine();

            Serie serieAtualizada = ObterEntradas(indiceSerie);

            repositorio.Atualiza(indiceSerie, serieAtualizada);
        }

        private static void ExcluirSerie()
        {
            int contagem = ListarSeries();

            if (contagem == 0) return;

            Console.WriteLine();
            int indiceSerie = ObterEntradaNumerica("Insira o id da série a ser excluída: ", 0, contagem - 1);

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            int contagem = ListarSeries();

            if (contagem == 0) return;

            Console.WriteLine();
            Console.Write("Insira o id da série a ser visualizada: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Serie serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie.ToString());
        }

        private static int ObterEntradaNumerica(string mensagem, int minimo, int maximo)
        {
            while (true)
            {
                try
                {
                    Console.Write(mensagem);
                    int numero = int.Parse(Console.ReadLine());

                    if (numero >= minimo && numero <= maximo) return numero;

                    Console.WriteLine("O valor excede os limites");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor, insira um número válido");
                }
            }
        }
        private static Serie ObterEntradas(int id)
        {
            Console.WriteLine("Gêneros disponíveis:");
            Console.WriteLine();

            Array generos = Enum.GetValues(typeof(Genero));

            foreach (int i in generos)
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine();

            int entradaGenero = ObterEntradaNumerica("Insira o gênero dentre as opções acima: ", 1, generos.Length - 1);

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            int entradaAno = ObterEntradaNumerica("Digite o ano de lançamento da série: ", 1980, 2050);

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: id,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            return novaSerie;
        }
    }
}
