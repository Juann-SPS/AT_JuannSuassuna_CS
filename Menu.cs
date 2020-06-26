using Pessoas.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenuT {
    public static class Menu {
        public static void MenuPrincipal() {
            Console.Clear();
            Console.WriteLine("Aniversariantes do Dia:");
            DiaAniversario();
            Console.WriteLine("_____________________________________");
            Console.WriteLine("Entre com uma das opcoes abaixo: ");
            Console.WriteLine("1 - Cadastrar Pessoa ");
            Console.WriteLine("2 - Buscar Pessoa ");
            Console.WriteLine("3 - Editar Pessoa ");
            Console.WriteLine("4 - Excluir Pessoa ");
            Console.WriteLine("5 - Sair ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao) {
                case 1:
                    CadastrarPessoa();
                    break;
                case 2:
                    BuscarPessoa();
                    break;
                case 3:
                    EditarPessoa();
                    break;
                case 4:
                    ExcluirPessoa();
                    break;
                case 5:
                    Console.WriteLine("Obrigado por fazer uso.");
                    break;
                default:
                    Console.WriteLine("Opcao nao encontrada, entre com uma existente.");
                    break;
            }
        }

        public static void CadastrarPessoa() {
            Console.Clear();

            Console.WriteLine("Entre com o nome: ");
            String nome = Console.ReadLine();
            Console.WriteLine("Entre com o sobrenome: ");
            String sobrenome = Console.ReadLine();

            DateTime aniversarioD = ConversorData();

            var pessoas = Dados.BusacarTodos();

            Pessoa p = new Pessoa(nome, sobrenome, aniversarioD);

            foreach (var pessoa in pessoas) {
                Pessoa ultimo = pessoas.Last(x => x.Id == pessoa.Id);
                p.Id = ultimo.Id + 1;
            }

            Console.WriteLine(p);
            Console.WriteLine("");
            Console.WriteLine("Os dados estao corretos ? (sim/nao) ");
            string opcao = Console.ReadLine();
            if (opcao == "sim") {
                Console.WriteLine("A pessoa foi adicionada com sucesso!");
                Dados.Salvar(p);
            }
            else {
                CadastrarPessoa();
            }
            VoltarProMenu();
        }

        public static void BuscarPessoa() {
            Console.Clear();
            Console.WriteLine("Entre com o nome de quem deseja buscar:");
            string[] NomeSobrenome = Console.ReadLine().Split(' ');
            string nome = NomeSobrenome[0];


            var ListaBuscaF = Dados.BusacarTodos(nome);

            if (ListaBuscaF.Count() == 0) {
                Console.WriteLine("Pessoa nao encontrada.");
            }
            else {
                Console.WriteLine("");
                Console.WriteLine("Pesssoas encontradas: ");

                foreach (var pessoa in ListaBuscaF) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobrenome);
                }

                Console.WriteLine("");
                Console.WriteLine("Digite o ID correspondente a quem deseja obter mais detalhes: ");
                int escolha = int.Parse(Console.ReadLine());

                foreach (var pessoa in ListaBuscaF) {
                    if (pessoa.Id == escolha) {
                        Console.WriteLine(Dados.BuscarPessoaPorId(escolha));

                    }
                }
            }
            VoltarProMenu();

        }

        private static void EditarPessoa() {
            Console.Clear();
            Console.WriteLine("Entre com o nome de quemm deseja editar:");
            string[] NomeSobrenome = Console.ReadLine().Split(' ');
            string nome = NomeSobrenome[0];


            var ListaBuscaF = Dados.BusacarTodos(nome);

            if (ListaBuscaF.Count() == 0) {
                Console.WriteLine("Pessoa nao encontrada");
            }
            else {
                Console.WriteLine("");
                Console.WriteLine("Pesssoas encontradas: ");

                foreach (var pessoa in ListaBuscaF) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobrenome);
                }

                Console.WriteLine("");
                Console.WriteLine("Digite o ID correspondente a quem deseja editar: ");
                int escolha = int.Parse(Console.ReadLine());

                foreach (var pessoa in ListaBuscaF) {
                    if (pessoa.Id == escolha) {
                        Pessoa PessoaPreA = Dados.BuscarPessoaPorId(escolha);
                        Console.WriteLine("Entre com o novo nome: ");
                        String NomeA = Console.ReadLine();
                        Console.WriteLine("Entre com o novo sobrenome: ");
                        String sobreNomeA = Console.ReadLine();
                        DateTime aniversarioN = ConversorData();

                        Pessoa PessoaPosA = new Pessoa(PessoaPreA.Id, NomeA, sobreNomeA, aniversarioN);

                        Dados.Editar(PessoaPosA);

                        Console.WriteLine("Dados alterados com sucesso!");
                    }
                }
            }
            VoltarProMenu();
        }

        private static void ExcluirPessoa() {
            Console.Clear();
            Console.WriteLine("Entre com o nome de quem deseja deletar:");
            string[] NomeSobrenome = Console.ReadLine().Split(' ');
            string nome = NomeSobrenome[0];

            var ListaBuscaF = Dados.BusacarTodos(nome);

            if (ListaBuscaF.Count() == 0) {
                Console.WriteLine("Pessoa nao encontrada");
            }
            else {
                Console.WriteLine("");
                Console.WriteLine("Pesssoas encontradas: ");
                foreach (var pessoa in ListaBuscaF) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobrenome);
                }


                Console.WriteLine("Digite o ID correspondente a quem deseja deletar: ");
                int escolha = int.Parse(Console.ReadLine());

                foreach (var pessoa in ListaBuscaF) {
                    if (pessoa.Id == escolha) {
                        Dados.Deletar(escolha);
                        Console.WriteLine("Pessoa deletada com sucesso!");
                    }
                }
            }
            VoltarProMenu();
        }


        private static void DiaAniversario() {
            DateTime hj = DateTime.Today;
            var niverToday = Dados.BusacarTodos(hj);
            if (niverToday.Count() == 0) {
                Console.WriteLine("Nenhum aniversario hoje!");
            }
            else {
                foreach (var pessoa in niverToday) {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobrenome);
                }
            }
        }

        

        public static DateTime ConversorData() {
            Console.WriteLine("Entre com a data de nascimento em dd/mm/yyyy: ");
            string data = Console.ReadLine();

            DateTime dataC = new DateTime();
            if (data.Contains("/")) {
                string[] vet = data.Split('/');
                int ano = int.Parse(vet[2]);
                int mes = int.Parse(vet[1]);
                int dia = int.Parse(vet[0]);
                dataC = new DateTime(ano, mes, dia);
            }
            else {
                Console.WriteLine("Entre com uma data valida.");
                VoltarProMenu();
            }
            return dataC;
        }

        public static void VoltarProMenu() {
            Console.WriteLine("Pressione qualquer tecla");
            Console.ReadKey();
            MenuPrincipal();
        }

        private static IDados Dados = new Dados();
    }
}