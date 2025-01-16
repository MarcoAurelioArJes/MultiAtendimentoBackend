using System.Security.Cryptography;

namespace MultiAtendimento.API.Services
{
    public class HashService
    {
        private const int CUSTO_DE_TRABALHO = 13;
        private const BCrypt.Net.HashType TIPO_DE_CRIPTOGRAFIA = BCrypt.Net.HashType.SHA256;

        public static string ObterSenhaHash(string senha)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(senha, CUSTO_DE_TRABALHO, TIPO_DE_CRIPTOGRAFIA);
        }

        public static bool ObterSeASenhaEhValida(string senhaInformada, string senhaHashNoBanco)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(senhaInformada, senhaHashNoBanco, TIPO_DE_CRIPTOGRAFIA);
        }
    }
}
