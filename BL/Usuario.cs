using DL;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(string userName, string password)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    ML.Result check = CheckUserName(userName);

                    if (check.Correct)
                    {
                        int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{userName}', " +
                        $"'{password}'");

                        if (query > 0)
                        {
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }//Add

        public static ML.Result GetByUserName(string userName, string password)
        {
            ML.Result result = new();
            ML.Usuario usuario = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    var query = context.Usuarios.FromSqlRaw
                        ($"UsuarioGetByUserName '{userName}', '{password}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Correct = true;
                        //result.Object = new object();
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }//GetByUserName

        public static ML.Result CheckUserName(string userName)
        {
            ML.Result result = new();

            try
            {
                using (DrosasUniSolutionsContext context = new())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioCheck '{userName}'").AsEnumerable().FirstOrDefault();

                    if (query == null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }//CheckUserName
    }
}