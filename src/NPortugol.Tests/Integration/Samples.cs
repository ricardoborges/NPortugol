using System;
using NPortugol.Modulos;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Integration
{
    [TestFixture]
    public class Samples
    {
        public class Pessoa
        {
            public string Nome { get; set; }
        }

        [Test]
        public void Propriedade()
        {
            #region script

            var script = @"fun��o nome(lista, i) vari�vel p p = lista[i] retorne p.Nome fim";

            #endregion

            var engine = new Motor();

            engine.Compilar(script);

            var lista = new object[] { new Pessoa { Nome = "Teste1" }, new Pessoa { Nome = "Teste2" }, new Pessoa { Nome = "Teste3" } };

            var nome = engine.Executar("nome", new object[] {lista, 1});

            Assert.AreEqual("Teste2", nome);
        }

        [Test]
        public void Fatorial()
        {
            #region script
            var script =
                @"
                fun��o fatorial(x)

                    se x == 0 retorne 1 fim

                    retorne fatorial(x - 1) * x 
                fim";
            #endregion

            var engine = new Motor();


            engine.Compilar(script);

            var r = engine.Executar("fatorial", 3);

            Assert.AreEqual(r, 6);
        }

        [Test]
        public void CPF()
        {
            #region script
            var script =
            @"fun��o valida(cpf)

                  vari�vel vet

                  vet = vetorizar(cpf)

	            se vet.tamanho < 11 retorne falso fim
	            se validaDigito(vet, vet[9], 9) == falso retorne falso fim
	            se validaDigito(vet, vet[10], 10) == falso retorne falso fim

	            retorne verdadeiro
            fim

            fun��o validaDigito(vet, dig, tam)
	            vari�vel soma, seq, digcalc, i, num, r

                    soma = 0
                    seq = tam + 1

	            para i = 0 at� tam
                       num = vet[i]
                       soma = soma + num * seq
                       seq = seq - 1
	            fim

                    r = soma % 11

                    se r < 2 
                        digcalc = 0 
                    sen�o
                        digcalc = 11 - r
                    fim

                    se digcalc == dig retorne verdadeiro fim


                    retorne falso
            fim";

            #endregion

            var engine = new Motor();


            engine.Compilar(script);

            var r = engine.Executar("valida", "11111111111");

            Assert.AreEqual(r, true);
        }


        [Test]
        public void Bolha()
        {
            #region script
            var script =
            @"fun��o ordenar(v)
              vari�vel i, j, t, tam

              tam = v.tamanho
              
              para t = 1 at� tam
                para i = 1 at� tam
                    j = i-1
                    se v[j] > v[i] ent�o
                      aux = v[i]
                      v[i] = v[j]
                      v[j] = aux         
                    fim
                fim
              fim

              retorne v
            fim";

            #endregion

            var engine = new Motor();

            engine.Compilar(script);

            var r = (object[])engine.Executar("ordenar", new object[] { new object[] { 4, 6, 2, 9, 5 } });

            Assert.AreEqual(2, Convert.ToInt32(r[0]));
        }


        [Test]
        public void Preco()
        {
            var script =
                @"fun��o melhor(a, g)

                    se a < g * 0.7 
                       retorne ""�lcool""
                    sen�o
                       retorne ""Gasolina""
                    fim
                fim";

            var engine = new Motor(new Npc());

            engine.Compilar(script);

            var r = engine.Executar("melhor", new object[]{3, 3});

            Assert.AreEqual("Gasolina", r);
        }
    }
}