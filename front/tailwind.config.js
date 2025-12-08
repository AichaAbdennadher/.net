/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'class',
    content: [
        "./**/*.razor",
        "./wwwroot/index.html"
    ],
    theme: {
        extend: {},
    },
    safelist: [
        // Inputs
        'w-full', 'px-5', 'py-3', 'border', 'rounded-md',
        'focus:outline-none', 'focus:ring-2', 'focus:ring-[#50bfa2',

        // Buttons
        'bg-[#50bfa2', 'text-white', 'font-semibold',
        'hover:brightness-110', 'transition',

        // Text
        'text-gray-500', 'dark:text-gray-400', 'text-sm', 'text-center',
        'text-3xl', 'font-bold', 'leading-relaxed', 'mb-4', 'mb-6',

        // Dark mode
        'dark:bg-gray-900', 'dark:bg-gray-800', 'dark:hover:bg-gray-700',

        // Layout & Misc
        'flex', 'flex-col', 'flex-row', 'items-center', 'justify-center',
        'max-w-5xl', 'min-h-screen', 'p-8', 'p-10', 'py-14', 'rounded-md',
        'shadow-2xl', 'bg-white', 'bg-gray-100', 'overflow-hidden',

        // Animations
        'animate-slide-in-left', 'animate-fade-in',

        // Slider
        'h-72', 'object-cover', 'flex-shrink-0',

         'w-16', 'w-24', 'w-32', 'w-40', 'w-48', 'w-56', 'w-64', 'w-full',
        'h-16', 'h-24', 'h-32', 'h-40', 'h-48', 'h-56', 'h-64', 'h-72', 'h-full',
        'mx-auto', 'my-auto', 'border-none'

    ],
    plugins: [],
}
