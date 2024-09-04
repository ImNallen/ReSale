/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.razor',
    './Layout/**/*.razor',
    './Components/**/*.razor',
    './wwwroot/**/*.html',
    './*.razor',
  ],
  theme: {
    fontFamily: {
      'body': ['"JetBrains Mono"', 'Roboto', 'sans-serif'],
    },
    extend: {
      colors: {
        'background-primary': '#1c1c1c',
        'background-secondary': '#2a2a2a',
        'green-primary': '#22a559',
        'green-secondary': '#17723d',
        'blue-primary': '#5765f2',
        'blue-secondary': '#444fbf',
        'red-primary': '#db5656',
        'red-secondary': '#b54747',
        'parchment': '#F2E8C9'
      },
      height: {
        'screen-50': 'calc(100vh - 50px)',
      },
    },
  },
  plugins: [],
}

