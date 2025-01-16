using System.ComponentModel;

namespace MultiAtendimento.API.Models.Enums
{
    public enum CargoEnum
    {
        [Description("Admin")]
        ADMIN,
        [Description("Atendente")]
        ATENDENTE,
        [Description("Cliente")]
        CLIENTE
    }
}
