using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DesafioWoop.GestaoSeguranca.API.Application.Queries
{
    public class QuestionarioUsuarioQueries : IQuestionarioUsuarioQueries
    {
        private string _connectionString = string.Empty;
        public QuestionarioUsuarioQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        
        public async Task<IEnumerable<QuestionarioUsuario>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return (await connection.QueryAsync<QuestionarioUsuario>("SELECT * FROM QuestionarioUsuario")).ToList();
            }
        }

        public async Task<IEnumerable<QuestionarioUsuario>> GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<QuestionarioUsuario>("SELECT A.* FROM QuestionarioUsuario A JOIN UserLogin B on A.UserLoginId = B.Id Where B.Email = @email", new { email });
            }
        }
    }
}
