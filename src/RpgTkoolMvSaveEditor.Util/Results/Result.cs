using System.Diagnostics.CodeAnalysis;

namespace RpgTkoolMvSaveEditor.Util.Results;
public abstract record Result;
public record Ok : Result;
public record Err(string Message = "") : Result;

public abstract record Result<T>
{
    /// <summary>
    /// Resultの中身を出す
    /// </summary>
    /// <param name="value">ResultがOkの時持つ値 Errの時null</param>
    /// <param name="message">ResultがErrの時持つメッセージ Okの時null</param>
    /// <returns>Okの時true Errの時false</returns>
    public bool Unwrap([NotNullWhen(true)] out T? value, [NotNullWhen(false)] out string? message)
    {
        if (this is Ok<T> ok)
        {
            value = ok.Value!;
            message = null;
            return true;
        }
        else if (this is Err<T> err)
        {
            value = default;
            message = err.Message;
            return false;
        }
        else { throw new NotImplementedException(); }
    }
}
public record Ok<T>(T Value) : Result<T>;
public record Err<T>(string Message = "") : Result<T>;
