using System;
using NPortugol.Modulos;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Integration
{
    [TestFixture]
    public class Samples
    {
        [Test]
        public void Fatorial()
        {
            var script =
                @"
função fatorial(x)

    se x == 0 retorne 1 fim

    retorne fatorial(x - 1) * x 
fim";

            var engine = new Motor(new Npc());


            engine.Compilar(script);

            var r = engine.Executar("fatorial", 3);

            Assert.AreEqual(r, 6);
        }

        [Test]
        public void CPF()
        {
            var script =
                @"função valida(cpf)

      variável vet

      vet = vetorizar(cpf)

	se vet.tamanho < 11 retorne falso fim
	se validaDigito(vet, vet[9], 9) == falso retorne falso fim
	se validaDigito(vet, vet[10], 10) == falso retorne falso fim

	retorne verdadeiro
fim

função validaDigito(vet, dig, tam)
	variável soma, seq, digcalc, i, num, r

        soma = 0
        seq = tam + 1

	para i = 0 até tam
           num = vet[i]
           soma = soma + num * seq
           seq = seq - 1
	fim

        r = soma % 11

        se r < 2 
            digcalc = 0 
        senão
            digcalc = 11 - r
        fim

        se digcalc == dig retorne verdadeiro fim


        retorne falso
fim";

            var engine = new Motor(new Npc());


            engine.Compilar(script);

            var r = engine.Executar("valida", "11111111111");

            Assert.AreEqual(r, true);
        }


        [Test]
        public void Bolha()
        {
            var script =
            @"função ordenar(v)
              variável i, j, t, tam

              tam = v.tamanho
              
              para t = 1 até tam
                para i = 1 até tam
                    j = i-1
                    se v[j] > v[i] então
                      aux = v[i]
                      v[i] = v[j]
                      v[j] = aux         
                    fim
                fim
              fim

              retorne v
            fim";

            var engine = new Motor(new Npc());

            engine.Compilar(script);

            var r = (object[])engine.Executar("ordenar", new object[] { new object[] { 4, 6, 2, 9, 5 } });

            Assert.AreEqual(2, Convert.ToInt32(r[0]));
        }


        [Test]
        public void Preco()
        {
            var script =
@"função melhor(a, g)

    se a < g * 0,7 
       retorne ""Álcool""
    senão
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