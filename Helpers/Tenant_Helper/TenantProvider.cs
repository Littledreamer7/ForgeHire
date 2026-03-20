using System.Security.Claims;

public class TenantProvider
{
    private readonly IHttpContextAccessor _http;

    public TenantProvider(IHttpContextAccessor http)
    {
        _http = http;
    }

    public int GetCompanyId()
    {
        var claim = _http.HttpContext?.User?.FindFirst("companyId");

        if (claim == null)
            throw new Exception("Tenant not found");

        return int.Parse(claim.Value);
    }

    public string GetRole()
    {
        return _http.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
    }

}