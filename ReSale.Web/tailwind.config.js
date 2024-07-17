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
        'darker-gray': '#2b2b2b',
        'dark-gray': '#3c3f41',
        'table-gray': '#303030',
        'light-gray': '#b4b9c0',
        'green-primary': '#22a559',
        'green-secondary': '#17723d',
        'blue-primary': '#5765f2',
        'blue-secondary': '#444fbf',
        'red-primary': '#db5656',
        'red-secondary': '#b54747',
      },
      height: {
        'screen-50': 'calc(100vh - 50px)',
      },
    },
  },
  plugins: [],
}

