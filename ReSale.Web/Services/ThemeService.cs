using System;
using Blazored.LocalStorage;

namespace ReSale.Web.Services;

public class ThemeService(ILocalStorageService localStorage)
{
#pragma warning disable CA1003
    public event Action? OnThemeChange;
#pragma warning restore CA1003

    public async Task<string> GetThemeAsync() => await localStorage.GetItemAsync<string>("theme") ?? "dark";

    public async Task SetThemeAsync(string theme)
    {
        await localStorage.SetItemAsync("theme", theme);
        OnThemeChange?.Invoke();
    }

    public async Task ToggleThemeAsync()
    {
        string currentTheme = await GetThemeAsync();
        string newTheme = currentTheme == "dark" ? "light" : "dark";
        await SetThemeAsync(newTheme);
    }
}
