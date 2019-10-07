using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.BussinessLogic
{
    public class MutanteLogic
    {

        public bool isMutant(string[] Array)
        {
            int Secuencia = 0;
            if (DiagonalesSecundarias(ref Secuencia, Array))
                return true;

            if (DiagonalesPrincipales(ref Secuencia, Array))
                return true;

            if (VerificacionHorizontal(ref Secuencia, Array))
                return true;

            if (VerificacionVertical(ref Secuencia, Array))
                return true;

            return false;
        }

        private bool DiagonalesSecundarias(ref int Secuencia, string[] Array)
        {
            Secuencia = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                string ValoresAnterioresTriSup = string.Empty, ValoresActualesTriSup = string.Empty;
                int SecuenciaTemporalSup = 1;

                for (int j = 0; j <= i; j++)
                {
                    ValoresAnterioresTriSup = ValoresActualesTriSup;
                    ValoresActualesTriSup = Array[i - j][j].ToString();

                    if (ValoresActualesTriSup.Equals(ValoresAnterioresTriSup))
                        SecuenciaTemporalSup++;
                }
                if (SecuenciaTemporalSup > 3) { Secuencia++; SecuenciaTemporalSup = 1; };
            }
            if (Secuencia > 1) return true;
            else
            {
                for (var i = 0; i < Array.Length; i++)
                {
                    string ValoresAnteriores = string.Empty;
                    string ValoresActuales = string.Empty;
                    int secTemp = 1;
                    for (var j = 0; j < Array.Length - i - 1; j++)
                    {
                        ValoresAnteriores = ValoresActuales;
                        ValoresActuales = Array[Array.Length - j - 1][j + i + 1].ToString();
                        if (ValoresActuales.Equals(ValoresAnteriores)) secTemp++;
                    }
                    if (secTemp > 3) Secuencia++;
                }
                if (Secuencia > 1) return true;
            }
            return false;
        }

        private bool DiagonalesPrincipales(ref int Secuencia, string[] Array)
        {

            for (int i = 0; i < Array.Length; i++)
            {
                string ValoresAnterioresTriSup = string.Empty, ValoresActualesTriSup = string.Empty;
                int SecuenciaTemporalSup = 1;

                for (int j = 0; j <= i; j++)
                {
                    ValoresAnterioresTriSup = ValoresActualesTriSup;

                    ValoresActualesTriSup = Array[(Array.Length - 1) - i + j][j].ToString();

                    if (ValoresActualesTriSup.Equals(ValoresAnterioresTriSup))
                        SecuenciaTemporalSup++;
                }
                if (SecuenciaTemporalSup > 3) { Secuencia++; SecuenciaTemporalSup = 1; }
            }
            if (Secuencia > 1) return true;
            else
            {
                for (var i = 0; i < Array.Length; i++)
                {
                    string ValoresAnteriores = string.Empty;
                    string ValoresActuales = string.Empty;
                    int secTemp = 1;
                    for (var j = 0; j < Array.Length - i - 1; j++)
                    {
                        ValoresAnteriores = ValoresActuales;
                        ValoresActuales = Array[j][i + j + 1].ToString();
                        if (ValoresActuales.Equals(ValoresAnteriores)) secTemp++;
                    }
                    if (secTemp > 3) { Secuencia++; secTemp = 1; }
                }
                if (Secuencia > 1) return true;
            }
            return false;
        }

        private bool VerificacionHorizontal(ref int Secuencia, string[] Array)
        {

            string ValorAnteriorHorizontal = string.Empty;
            string ValorActualHorizontal = string.Empty;
            for (int i = 0; i < Array.Length; i++)
            {
                int secuenciaTemHo = 1;
                ValorAnteriorHorizontal = ValorActualHorizontal = string.Empty;
                for (int j = 0; j < Array.Length; j++)
                {

                    ValorAnteriorHorizontal = ValorActualHorizontal;
                    ValorActualHorizontal = Array[i].Substring(j, 1);
                    if (ValorActualHorizontal.Equals(ValorAnteriorHorizontal))
                        secuenciaTemHo++;

                }
                if (secuenciaTemHo > 3)
                {
                    secuenciaTemHo = 1;
                    Secuencia++;
                }

                if (Secuencia > 1)
                    return true;

            }
            return false;
        }

        private bool VerificacionVertical(ref int Secuencia, string[] Array)
        {


            string ValorAnteriorVertical = string.Empty;
            string ValorActualVertical = string.Empty;
            for (int i = 0; i < Array.Length; i++)
            {
                int SecuenciaTenVe = 1;
                for (int j = 0; j < Array.Length; j++)
                {
                    ValorAnteriorVertical = ValorActualVertical;
                    ValorActualVertical = Array[j].Substring(i, 1);
                    if (ValorActualVertical.Equals(ValorAnteriorVertical))
                        SecuenciaTenVe++;
                }
                if (SecuenciaTenVe > 3)
                {
                    Secuencia++;
                    SecuenciaTenVe = 1;
                }

                if (Secuencia > 1)

                    return true;


            }
            return false;

        }

        public bool VerificacionDeLetras(string[] Array)
        {
            string LETRASPERMITIDAS = "ATGCA";
            for (int i = 0; i < Array.Length; i++)
            {
                for (int j = 0; j < Array.Length; j++)
                {
                    if (!LETRASPERMITIDAS.Contains(Array[i][j]))
                        return false;
                }

            }
            return true;

        }

        public bool ValidarMatrizNXN(string[] Array)
        {          
            foreach (var item in Array)            
            if (item.Length!= Array.Length)
            return false;
            return true;

        }

    }
}