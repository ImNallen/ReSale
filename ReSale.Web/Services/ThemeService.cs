using System;
using Blazored.LocalStorage;

namespace ReSale.Web.Services;

public class ThemeService(ILocalStorageService localStorage)
{
#pragma warning disable CA1003
    public event Action? OnThemeChange;
#pragma warning restore CA1003

    public bool IsDarkMode { get; private set; }

    public async Task<string> GetThemeAsync()
    {
        string theme = await localStorage.GetItemAsync<string>("theme") ?? "dark";
        IsDarkMode = theme == "dark";
        return theme;
    }

    public async Task SetThemeAsync(string theme)
    {
        await localStorage.SetItemAsync("theme", theme);
        IsDarkMode = theme == "dark";
        OnThemeChange?.Invoke();
    }

    public async Task ToggleThemeAsync()
    {
        string currentTheme = await GetThemeAsync();
        string newTheme = currentTheme == "dark" ? "light" : "dark";
        await SetThemeAsync(newTheme);
    }
}
