using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace CSVConsoleSisap
{
    class testes
    {
        public static string arqIN = "p.csv";
        public static string arqOUT= "saida.csv";
        public static readonly string[] headerPadrao = new string[]     //Cabeçalho com todos os campos possíveis de importação
        {
            "Nome",                         //Index: 0
            "Sexo",                         //Index: 1
            "Nome_Destinatario",            //Index: 2
            "Tratamento",                   //Index: 3
            "Data_Nascimento",              //Index: 4
            "Logradouro_1",                 //Index: 5
            "Bairro_1",                     //Index: 6
            "Localidade_1",                 //Index: 7
            "Cep_1",                        //Index: 8
            "Logradouro_2",                 //Index: 9
            "Bairro_2",                     //Index: 10
            "Localidade_2",                 //Index: 11
            "Cep_2",                        //Index: 12
            "Logradouro_3",                 //Index: 13
            "Bairro_3",                     //Index: 14
            "Localidade_3",                 //Index: 15
            "Cep_3",                        //Index: 16
            "Logradouro_4",                 //Index: 17
            "Bairro_4",                     //Index: 18
            "Localidade_4",                 //Index: 19
            "Cep_4",                        //Index: 20
            "Logradouro_5",                 //Index: 21
            "Bairro_5",                     //Index: 22
            "Localidade_5",                 //Index: 23
            "Cep_5",                        //Index: 24
            "Tipo_Telefone_1",              //Index: 25
            "Valor_Telefone_1",             //Index: 26
            "Observacao_Telefone_1",        //Index: 27
            "Tipo_Telefone_2",              //Index: 28
            "Valor_Telefone_2",             //Index: 29
            "Observacao_Telefone_2",        //Index: 30
            "Tipo_Telefone_3",              //Index: 31
            "Valor_Telefone_3",             //Index: 32
            "Observacao_Telefone_3",        //Index: 33
            "Tipo_Telefone_4",              //Index: 34
            "Valor_Telefone_4",             //Index: 35
            "Observacao_Telefone_4",        //Index: 36
            "Tipo_Telefone_5",              //Index: 37
            "Valor_Telefone_5",             //Index: 38
            "Observacao_Telefone_5",        //Index: 39
            "Tipo_Telefone_6",              //Index: 40
            "Valor_Telefone_6",             //Index: 41
            "Observacao_Telefone_6",        //Index: 42
            "Tipo_Telefone_7",              //Index: 43
            "Valor_Telefone_7",             //Index: 44
            "Observacao_Telefone_7",        //Index: 45
            "Tipo_Telefone_8",              //Index: 46
            "Valor_Telefone_8",             //Index: 47
            "Observacao_Telefone_8",        //Index: 48
            "Tipo_Telefone_9",              //Index: 49
            "Valor_Telefone_9",             //Index: 50
            "Observacao_Telefone_9",        //Index: 51
            "Tipo_Endereco_Eletronico_1",   //Index: 52
            "Valor_Endereco_Eletronico_1",  //Index: 53
            "Tipo_Endereco_Eletronico_2",   //Index: 54
            "Valor_Endereco_Eletronico_2",  //Index: 55
            "Tipo_Endereco_Eletronico_3",   //Index: 56
            "Valor_Endereco_Eletronico_3",  //Index: 57
            "Tipo_Endereco_Eletronico_4",   //Index: 58
            "Valor_Endereco_Eletronico_4",  //Index: 59
            "Tipo_Endereco_Eletronico_5",   //Index: 60
            "Valor_Endereco_Eletronico_5",  //Index: 61
            "Tipo_Endereco_Eletronico_6",   //Index: 62
            "Valor_Endereco_Eletronico_6",  //Index: 63
            "Tipo_Endereco_Eletronico_7",   //Index: 64
            "Valor_Endereco_Eletronico_7",  //Index: 65
            "Tipo_Endereco_Eletronico_8",   //Index: 66
            "Valor_Endereco_Eletronico_8",  //Index: 67
            "Tipo_Endereco_Eletronico_9",   //Index: 68
            "Valor_Endereco_Eletronico_9",  //Index: 69
            "Grupo_1",                      //Index: 70
            "Grupo_2",                      //Index: 71
            "Grupo_3",                      //Index: 72
            "Grupo_4",                      //Index: 73
            "Grupo_5",                      //Index: 74
            "Grupo_6",                      //Index: 75
            "Grupo_7",                      //Index: 76
            "Grupo_8",                      //Index: 77
            "Grupo_9",                      //Index: 78
            "Partido_Politico",             //Index: 79
            "Profissao",                    //Index: 80
            "Observacao",                   //Index: 81
            "Cargo_1",                      //Index: 82
            "Cargo_2",                      //Index: 83
            "Cargo_3",                      //Index: 84
            "Cargo_4",                      //Index: 85
            "Cargo_5",                      //Index: 86
            "Cargo_6",                      //Index: 87
            "Cargo_7",                      //Index: 88
            "Cargo_8",                      //Index: 89
            "Cargo_9",                      //Index: 90
            "Dia",                          //Index: 91 - Para nascimento com data separada
            "Mes",                          //Index: 92 - Para nascimento com data separada
            "Ano",                          //Index: 93 - Para nascimento com data separada
            "ERROS"                         //Index: 94 - Erros encontrados
        }; //Cabeçalho padrão com todos campos disponíveis possíveis
        public static List<int> indexCampos = new List<int>();          
        public static int TotalDeLinhasArquivo = 0;
        public class Nome { 
            public string nome { get; set; }
            public Boolean M { get; set; }
            public Boolean F { get; set; }
        }
        public class Arquivo { 
            public string nome { get; set; }
            public int contLinhas { get; set; }
        }

        static void Main2(string[] args)
        {
            Console.WriteLine("INICIO");

            
        }
    }

}
