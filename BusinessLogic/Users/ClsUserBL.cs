

using DataAccess.DataBase;
using Entity.Users;
using System;
using System.Data;

namespace BusinessLogic.Users
{
    public class ClsUserBL
    {
        #region Private Variable

        ClsDataBase Objdatabase = null;


        #endregion


        #region Void Index
        public void Index(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[SP_Usuarios_Index]",
                Scalar = false,
            };
            Execute(ref ObjUser);
        }

        #endregion
        #region User CRUD 
        public void Create(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[Sp_Usuarios_Create]",
                Scalar = true,
            };
            Objdatabase.DtParameters.Rows.Add(@"@Nombre","17", ObjUser.Nombre1);
            Objdatabase.DtParameters.Rows.Add(@"@Primer_Apellido", "17", ObjUser.Primer_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@Segundo_Apellido", "17", ObjUser.Segundo_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@FechaNacimiento", "13", ObjUser.FechaNacimiento1);
            Objdatabase.DtParameters.Rows.Add(@"@Estado", "1", ObjUser.Estado1);
            Execute(ref ObjUser);
        }
        public void Read(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[Sp_Usuarios_Read]",
                Scalar = false,
            };

            Objdatabase.DtParameters.Rows.Add(@"@Idusuario", "2", ObjUser.Idusuario1);

            Execute(ref ObjUser);
        }
        public void Update(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[Sp_Usuarios_Update]",
                Scalar = true,
            };
            Objdatabase.DtParameters.Rows.Add(@"@Idusuario", "2", ObjUser.Idusuario1);
            Objdatabase.DtParameters.Rows.Add(@"@Nombre", "17", ObjUser.Nombre1);
            Objdatabase.DtParameters.Rows.Add(@"@Primer_Apellido", "17", ObjUser.Primer_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@Segundo_Apellido", "17", ObjUser.Segundo_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@FechaNacimiento", "13", ObjUser.FechaNacimiento1);
            Objdatabase.DtParameters.Rows.Add(@"@Estado", "1", ObjUser.Estado1);

            Execute(ref ObjUser);
        }
        public void Delete(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[Sp_Usuarios_Delete]",
                Scalar = true,
            };
            Objdatabase.DtParameters.Rows.Add(@"@Idusuario", "2", ObjUser.Idusuario1);

            Execute(ref ObjUser);
        }
        #endregion
        #region Private Voids
        private void Execute(ref CLsUserE Objuser)
        {
            Objdatabase.CRUD(ref Objdatabase);

            if(Objdatabase.MessageErrorDB == null)
            {
                if (Objdatabase.Scalar)
                {
                    Objuser.ValorScalar1 = Objdatabase.ValorScalar;
                }
                else
                {
                    Objuser.DtResults = Objdatabase.DsResult.Tables[0];
                    if(Objuser.DtResults.Rows.Count == 1)
                    {
                    foreach(DataRow item in Objuser.DtResults.Rows)
                        {
                            Objuser.Idusuario1 = Convert.ToByte(item["Idusuario"].ToString());
                            Objuser.Nombre1 = item["Nombre"].ToString();
                            Objuser.Primer_Apellido1 = item["Primer_Apellido"].ToString();
                            Objuser.Segundo_Apellido1 = item["Segundo_Apellido"].ToString();
                            Objuser.FechaNacimiento1 = Convert.ToDateTime(item["FechaNacimiento"].ToString());
                            Objuser.Estado1 = Convert.ToBoolean(item["Estado"].ToString());


                        }
                    }
                }

            }
            else
            {
                Objuser.MessageError= Objdatabase.MessageErrorDB;
            }

        }

        #endregion
    }
}
