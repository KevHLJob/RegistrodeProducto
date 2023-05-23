
using System;
using System.Data;

namespace Entity.Users
{
    public class CLsUserE
    {
        #region  private Attributes

        private byte Idusuario;
        private string Nombre,Primer_Apellido,Segundo_Apellido;
        private DateTime FechaNacimiento;
        private bool Estado;

        // Attributes of DB comunication
        private string messageError, ValorScalar;
        private DataTable dtResults;


        #endregion

        #region  public Attributes
        public byte Idusuario1 { get => Idusuario; set => Idusuario = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Primer_Apellido1 { get => Primer_Apellido; set => Primer_Apellido = value; }
        public string Segundo_Apellido1 { get => Segundo_Apellido; set => Segundo_Apellido = value; }
        public DateTime FechaNacimiento1 { get => FechaNacimiento; set => FechaNacimiento = value; }
        public bool Estado1 { get => Estado; set => Estado = value; }
        public string MessageError { get => messageError; set => messageError = value; }
        public string ValorScalar1 { get => ValorScalar; set => ValorScalar = value; }
        public DataTable DtResults { get => dtResults; set => dtResults = value; }

        #endregion
    }
}
