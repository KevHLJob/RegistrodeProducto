

using DataAccess.DataBase; //referencia clase dataaccess
using Entity.Users;//referencia a clase entity
using System;
using System.Data;

namespace BusinessLogic.Users
{
    public class ClsUserBL
    {
        #region Private Variable
        //inicializamos el objeto en null
        ClsDataBase Objdatabase = null;


        #endregion


        #region Void Index
        //metodo publico referencia de la clase entity
        public void Index(ref CLsUserE ObjUser)
        {
            //instancia el objeto database
            Objdatabase = new ClsDataBase()
            {
                //se especifica el nombre de la tabla y el proceso almacenado
                TableName = "Usuarios",
                NameSp = "[SCH_General].[SP_Usuarios_Index]",
                Scalar = false,
            };
            //se ejecuta el objeto referenciado
            Execute(ref ObjUser);
        }

        #endregion
        #region User CRUD 
        //metodo de creación en la base de datos
        public void Create(ref CLsUserE ObjUser)
        {
            Objdatabase = new ClsDataBase()
            {
                TableName = "Usuarios",
                NameSp = "[SCH_General].[Sp_Usuarios_Create]",
                Scalar = true,
            };
            //parametros con los nombres de la columna, el numero es el tipo de dato, variables de la clase entity 
            Objdatabase.DtParameters.Rows.Add(@"@Nombre","17", ObjUser.Nombre1);
            Objdatabase.DtParameters.Rows.Add(@"@Primer_Apellido", "17", ObjUser.Primer_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@Segundo_Apellido", "17", ObjUser.Segundo_Apellido1);
            Objdatabase.DtParameters.Rows.Add(@"@FechaNacimiento", "13", ObjUser.FechaNacimiento1);
            Objdatabase.DtParameters.Rows.Add(@"@Estado", "1", ObjUser.Estado1);
            Execute(ref ObjUser);
        }
        //metodo de lectura, por medio de un id seleccionado
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
        //metodo de actualizacion
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
        //metodo de eliminar registro, por medio de un id
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
