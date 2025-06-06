using Auth.Models.InModels;
using Auth.Models.OutModels;
using Auth.Services.Contract;
using Auth.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Auth.Services.Implement
{
    public class Auth_Service : IAuth_Service
    {
        private readonly IConfiguration _configuration;

        public Auth_Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseManager> AddUser(AddUserIM addUserIM)
        {
            try
            {
                var usuario = new UserOM();

                using (var cnn = new SqlConnection(_configuration["ConnectionStrings:cnn"]))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Pwa_Add_Users", cnn);
                    cmd.Parameters.AddWithValue("strDni", addUserIM.strDni);
                    cmd.Parameters.AddWithValue("strName ", addUserIM.strName);
                    cmd.Parameters.AddWithValue("strLastName ", addUserIM.strLastName);
                    cmd.Parameters.AddWithValue("strPassword ", addUserIM.strPassword);
                    cmd.Parameters.AddWithValue("strEmail ", addUserIM.strEmail);
                    cmd.Parameters.AddWithValue("strPhone ", addUserIM.strPhone);
                    cmd.Parameters.AddWithValue("strBirthDate ", addUserIM.strBirthDate);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            usuario.intUserId = Convert.ToInt32(dr["intUserId"]);
                            usuario.strDni = dr["strDni"].ToString();
                            usuario.strName = dr["strName"].ToString();
                            usuario.strLastName = dr["strLastName"].ToString();
                            usuario.strPassword = dr["strPassword"].ToString();
                            usuario.strEmail = dr["strEmail"].ToString();
                            usuario.strPhone = dr["strPhone"].ToString();
                            usuario.BirthDate = dr["strBirthDate"].ToString();
                        }
                    }
                }

                if (usuario.intUserId == 0)
                {
                    return new ResponseManager
                    {
                        IsSuccess = false,
                        Message = "No se pudo crear el usuario. Es posible que ya exista ",
                        Data = usuario
                    };

                }

                return new ResponseManager
                {
                    IsSuccess = true,
                    Message = "Usuario creado exitosamente.",
                    Data = usuario
                };

            }
            catch (Exception ex)
            {
                return new ResponseManager
                {
                    IsSuccess = false,
                    Message = "Error en el método AddUser ",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseManager> Login(LoginIM loginIM)
        {
            try
            {
                var usuario = new UserOM();

                using (var cnn = new SqlConnection(_configuration["ConnectionStrings:cnn"]))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Pwa_Security_Login", cnn);
                    cmd.Parameters.AddWithValue("strEmail", loginIM.strEmail);
                    cmd.Parameters.AddWithValue("strPassword ", loginIM.strPassword);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            usuario.intUserId = Convert.ToInt32(dr["intUserId"]);
                            usuario.strDni = dr["strDni"].ToString();
                            usuario.strName = dr["strName"].ToString();
                            usuario.strLastName = dr["strLastName"].ToString();
                            usuario.strPassword = dr["strPassword"].ToString();
                            usuario.strEmail = dr["strEmail"].ToString();
                            usuario.strPhone = dr["strPhone"].ToString();
                            usuario.BirthDate = dr["strBirthDate"].ToString();
                        }
                    }
                }

                if (usuario.intUserId == 0)
                {
                    return new ResponseManager
                    {
                        IsSuccess = false,
                        Message = "El usuario no existe",
                        Data = usuario
                    };

                }

                return new ResponseManager
                {
                    IsSuccess = true,
                    Message = "login Exitoso",
                    Data = usuario
                };

            }
            catch (Exception ex)
            {
                return new ResponseManager
                {
                    IsSuccess = false,
                    Message = "Error en el método Login ",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseManager> GetUsers()
        {
            try
            {
                var usuario = new UserOM();

                using (var cnn = new SqlConnection(_configuration["ConnectionStrings:cnn"]))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Pwa_Security_Get_Users", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            usuario.intUserId = Convert.ToInt32(dr["intUserId"]);
                            usuario.strDni = dr["strDni"].ToString();
                            usuario.strName = dr["strName"].ToString();
                            usuario.strLastName = dr["strLastName"].ToString();
                            usuario.strPassword = dr["strPassword"].ToString();
                            usuario.strEmail = dr["strEmail"].ToString();
                            usuario.strPhone = dr["strPhone"].ToString();
                            usuario.BirthDate = dr["strBirthDate"].ToString();
                        }
                    }
                }

                if (usuario.intUserId == 0)
                {
                    return new ResponseManager
                    {
                        IsSuccess = false,
                        Message = "No hay datos para mostrar",
                        Data = usuario
                    };

                }

                return new ResponseManager
                {
                    IsSuccess = true,
                    Message = "Consulta realizada con éxito",
                    Data = usuario
                };

            }
            catch (Exception ex)
            {
                return new ResponseManager
                {
                    IsSuccess = false,
                    Message = "Error en el método GetUsers ",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseManager> ChangePassword(ChangePasswordIM changePasswordIM)
        {
            try
            {
                var usuario = new UserOM();

                using (var cnn = new SqlConnection(_configuration["ConnectionStrings:cnn"]))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Pwa_Security_ChangePassword", cnn);
                    cmd.Parameters.AddWithValue("strEmail", changePasswordIM.strEmail);
                    cmd.Parameters.AddWithValue("strCurrentPassword", changePasswordIM.strCurrentPassword);
                    cmd.Parameters.AddWithValue("strNewPassword", changePasswordIM.strNewPassword);

                    cmd.CommandType = CommandType.StoredProcedure;

                    var response = await cmd.ExecuteReaderAsync();

                    return new ResponseManager
                    {
                        IsSuccess = true,
                        Message = "Contraseña actualizada correctamente",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseManager
                {
                    IsSuccess = false,
                    Message = "Error en el método ChangePassword ",
                    Data = ex.Message
                };
            }
        }

        public async Task<ResponseManager> DeleteUser(DeleteUserIM deleteUserIM)
        {
            try
            {
                var usuario = new UserOM();

                using (var cnn = new SqlConnection(_configuration["ConnectionStrings:cnn"]))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Pwa_Delete_User", cnn);
                    cmd.Parameters.AddWithValue("intUserId", deleteUserIM.intUserId);

                    cmd.CommandType = CommandType.StoredProcedure;

                    var response = await cmd.ExecuteReaderAsync();

                    return new ResponseManager
                    {
                        IsSuccess = true,
                        Message = "El usuario fue eliminado correctamente",
                        Data = null
                    };

                }
            }
            catch (Exception ex)
            {
                return new ResponseManager
                {
                    IsSuccess = false,
                    Message = "Error en el método DeleteUser ",
                    Data = ex.Message
                };
            }
        }
    }
}


