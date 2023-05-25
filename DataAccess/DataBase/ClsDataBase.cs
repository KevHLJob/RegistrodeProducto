using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DataBase
{
    public class ClsDataBase
    {
        #region Private Variables
        private SqlConnection objsqlConnection;
        private SqlDataAdapter objsqlAdapter; //permite la lectura de datos
        private SqlCommand objsqlCommand;//permite enviar los querys
        private DataSet dsResult; //Lista de tables
        private DataTable dtParameters; //para los procesos almacenados de la BD
        private String tableName, nameSp, messageErrorDB, valorScalar, nameDB;
        //valor scalar: Por si es necesario retornar un valor
        private bool scalar;


        #endregion


        #region Public Variables

        public SqlConnection ObjsqlConnection { get => objsqlConnection; set => objsqlConnection = value; }
        public SqlDataAdapter ObjsqlAdapter { get => objsqlAdapter; set => objsqlAdapter = value; }
        public SqlCommand ObjsqlCommand { get => objsqlCommand; set => objsqlCommand = value; }
        public DataSet DsResult { get => dsResult; set => dsResult = value; }
        public DataTable DtParameters { get => dtParameters; set => dtParameters = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public string NameSp { get => nameSp; set => nameSp = value; }
        public string MessageErrorDB { get => messageErrorDB; set => messageErrorDB = value; }
        public string ValorScalar { get => valorScalar; set => valorScalar = value; }
        public string NameDB { get => nameDB; set => nameDB = value; }
        public bool Scalar { get => scalar; set => scalar = value; }

        #endregion

        #region Constructor
        //Para inicializar los valores que sean necesarios
        public ClsDataBase()
        {
            DtParameters = new DataTable("SpParameters");
            DtParameters.Columns.Add("Nombre");
            DtParameters.Columns.Add("TipoDeDato");
            DtParameters.Columns.Add("Valor");


            NameDB = "DB_BasePruebas";
        }

        #endregion

        #region Private Void
        //metodo privado para crear la conexion, referenciando un objetodatabase
        private void Createconnection(ref ClsDataBase objDataBase)
        {
            //obtenemos el objeto NameDB, en caso de que cumpla con la condición
            switch (objDataBase.NameDB)
            {
                case "DB_BasePruebas":
                    //Crea la conexion por medio del nombre obtenido y la cadena conexion
                    objDataBase.objsqlConnection = new SqlConnection
                        (Properties.Settings.Default.cadenaConection_DB_BasePruebas);
                    break;
                default:
                    break;
            }
        }
        private void ValidationconnectionDB(ref ClsDataBase objDataBase)
        {
            //validacion si el estado es cerrado, se abre la conexion a BD
            if (objDataBase.objsqlConnection.State == ConnectionState.Closed)
            {
                objDataBase.objsqlConnection.Open();
            }
            else
            {
                //sino esta abierta la cierra y la quita de memoria.
                objDataBase.objsqlConnection.Close();
                objDataBase.objsqlConnection.Dispose();
            }
        }
        private void AddParameters(ref ClsDataBase objDataBase)
        {
            // si es diferente o igual a null
            if (objDataBase.dtParameters != null)
            {
                SqlDbType DataTypeSQL = new SqlDbType();
                // realiza un recorrido por item en cada campo para verificar que tipo de datos es
                foreach (DataRow item in objDataBase.DtParameters.Rows)
                {
                    switch (item[1])
                    {
                        case "1":
                            DataTypeSQL = SqlDbType.Bit;
                            break;
                        case "2":
                            DataTypeSQL = SqlDbType.TinyInt;
                            break;
                        case "3":
                            DataTypeSQL = SqlDbType.SmallInt;
                            break;
                        case "4":
                            DataTypeSQL = SqlDbType.Int;
                            break;
                        case "5":
                            DataTypeSQL = SqlDbType.BigInt;
                            break;
                        case "6":
                            DataTypeSQL = SqlDbType.Decimal;
                            break;
                        case "7":
                            DataTypeSQL = SqlDbType.SmallMoney;
                            break;
                        case "8":
                            DataTypeSQL = SqlDbType.Money;
                            break;
                        case "9":
                            DataTypeSQL = SqlDbType.Float;
                            break;
                        case "10":
                            DataTypeSQL = SqlDbType.Real;
                            break;
                        case "11":
                            DataTypeSQL = SqlDbType.Date;
                            break;
                        case "12":
                            DataTypeSQL = SqlDbType.Time;
                            break;
                        case "13":
                            DataTypeSQL = SqlDbType.SmallDateTime;
                            break;
                        case "14":
                            DataTypeSQL = SqlDbType.Date;
                            break;
                        case "15":
                            DataTypeSQL = SqlDbType.Char;
                            break;
                        case "16":
                            DataTypeSQL = SqlDbType.NChar;
                            break;
                        case "17":
                            DataTypeSQL = SqlDbType.VarChar;
                            break;
                        case "18":
                            DataTypeSQL = SqlDbType.NVarChar;
                            break;

                        default:
                            break;
                    }
                    // si es escalar
                    if (objDataBase.Scalar)
                    {
                        
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            objDataBase.objsqlCommand.Parameters.Add(item[0].ToString(), DataTypeSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            objDataBase.objsqlCommand.Parameters.Add(item[0].ToString(), DataTypeSQL).Value = item[2].ToString();

                        }
                    }
                    else
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            objDataBase.objsqlAdapter.SelectCommand.Parameters.Add(item[0].ToString(), DataTypeSQL).Value = DBNull.Value;
                        }
                        else
                        {
                            objDataBase.objsqlAdapter.SelectCommand.Parameters.Add(item[0].ToString(), DataTypeSQL).Value = item[2].ToString();

                        }
                    }
                }
            }
        }
        private void PrepareconnectionDataBase(ref ClsDataBase objDataBase)
        {
            //se llama el metodo de creación referenciando el objeto database
            Createconnection(ref objDataBase);
            //llama el metodo de creación referenciando el objeto database
            ValidationconnectionDB(ref objDataBase);

        }
        private void ExecuteDataAdapter(ref ClsDataBase objDataBase)
        {
            //try para capturar cualquier error
            try
            {
                //llamada del metodo de preparar conexion
                PrepareconnectionDataBase(ref objDataBase);
                //se crea un nuevo sql data adapter para pasarle el nombre del proceso almacenado y la conexion
                objDataBase.objsqlAdapter = new SqlDataAdapter(objDataBase.nameSp, objDataBase.objsqlConnection);
                objDataBase.objsqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AddParameters(ref objDataBase);
                // se instancia, los datos obtenidos y la tabla se llenan con obj sql adapter
                objDataBase.dsResult = new DataSet();
                objDataBase.objsqlAdapter.Fill(objDataBase.dsResult, objDataBase.tableName);
            }
            catch (Exception ex)
            {
                objDataBase.messageErrorDB = ex.Message.ToString();
            }
            finally
            {
                if (objDataBase.objsqlConnection.State == ConnectionState.Open)
                {
                    ValidationconnectionDB(ref objDataBase);
                }
            }
        }
        private void ExecuteCommand(ref ClsDataBase objDataBase)
        {
            try
            {
                //prepara la conexion de la base de datos, referencia al objeto database
                PrepareconnectionDataBase(ref objDataBase);
                objDataBase.objsqlCommand = new SqlCommand(objDataBase.nameSp, objDataBase.objsqlConnection);
                objDataBase.objsqlCommand.CommandType = CommandType.StoredProcedure;
                AddParameters(ref objDataBase);

                //si el objeto es scalar true
                if (objDataBase.scalar)
                {
                    // en valorscalar se guarda el executeScalar
                    objDataBase.valorScalar = objDataBase.objsqlCommand.ExecuteScalar().ToString().Trim();

                }
                else
                {
                    //sino ejecuta el command execute scalar del objeto referenciado
                    objDataBase.objsqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                objDataBase.messageErrorDB = ex.Message.ToString();
            }
            finally
            {
                if (objDataBase.objsqlConnection.State == ConnectionState.Open)
                {
                    ValidationconnectionDB(ref objDataBase);
                }
            }
        }

        #endregion

        #region Public Void
        public void CRUD(ref ClsDataBase objDataBase)
        {
            // si el objeto es true scalar
            if (objDataBase.scalar)
            {
               //ejecuta el command del objeto database
                ExecuteCommand(ref objDataBase);
            }
            else
            {
                //sino el data adapter del objeto database
                ExecuteDataAdapter(ref objDataBase);
            }
        }

        #endregion
    }
}
