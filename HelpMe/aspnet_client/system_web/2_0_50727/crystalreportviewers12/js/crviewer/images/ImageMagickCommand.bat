REM You must have ImageMagick installed to run this command.  It will combine all the images into a single file "toolbar.gif".
REM The order of the image files below is critical and must be in sync with the definitions in toolbar.js.

convert -append export.gif print.gif grouptree_toggle.gif panel_toggle.gif first.gif prev.gif next.gif last.gif refresh.gif search.gif toolbar.gif