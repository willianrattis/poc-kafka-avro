namespace Models;

public class ExibicaoPDP
{
    public string DeviceId { get; set; }
    public string Bandeira { get; set; }
    public string ProdutoId { get; set; }
    public double PrecoSite { get; set; }
    public double PrecoDesconto { get; set; }
    public string Sku { get; set; }
    public string Origem { get; set; }
    public string DataEvento { get; set; }
    public Usuario Usuario { get; set; }
}

public class Usuario
{
    /// <summary>
    /// E-mail do usuário.
    /// </summary>
    public string Email { get; set; } = "guest@user.com";
    /// <summary>
    /// Id do usuário. Deve ser o mesmo identificador utilizado no site. ou ID Unico usuario
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Informa se o cliente autoriza recebimento de campanhas de email.
    /// </summary>
    public string PermiteEmailMarketing => "false";
}