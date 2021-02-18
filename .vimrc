" colorscheme hybrid_material
" colorscheme sierra
" colorscheme materialbox
" colorscheme solarized8_flat
" colorscheme space-vim-dark
" colorscheme tender
" colorscheme xcodedark
" colorscheme seoul256-light

if $TERM == 'xterm-kitty'
    autocmd VimEnter * NERDTree
    autocmd VimEnter * wincmd p
endif

" set background=light
set nocompatible
set backspace=2
set ts=4 sw=4
set mouse=a

" highlight SignColumn ctermbg=0 guibg=lightgrey
" set signcolumn=no
