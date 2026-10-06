namespace ProjectApp.WebUI.Base;

// API'deki BaseResult<T>'nin WebUI tarafındaki karşılığı.
// API'den gelen JSON bu sınıfa dönüştürülecek.
public class ApiResult<T>
{
    public T? Data { get; set; }
    public List<ApiError> Errors { get; set; } = [];
    public bool IsSuccessful { get; set; }
}

public class ApiError
{
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}