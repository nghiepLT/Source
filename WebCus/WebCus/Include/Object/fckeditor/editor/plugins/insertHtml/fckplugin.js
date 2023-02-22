/*********************************************************************************************************/
/**
 * Fckeditor Insert Html Code Plugin (Support: Lajox ; Email: lajox@19www.com)
 * 
 * 
 * Download: http://code.google.com/p/lajox
 */
/*********************************************************************************************************/

// Register the related command.
FCKCommands.RegisterCommand( 'insertHtml', new FCKDialogCommand( 'insertHtml', FCKLang.insertHtml, FCKPlugins.Items['insertHtml'].Path + 'fck_insertHtml.html', 415, 300 ) ) ;

// Create the "insertHtml" toolbar button.
var oinsertHtmlItem = new FCKToolbarButton( 'insertHtml', FCKLang.insertHtml, FCKLang.insertHtml, null, null, false, true) ;
oinsertHtmlItem.IconPath = FCKPlugins.Items['insertHtml'].Path + 'insertHtml.gif' ;

FCKToolbarItems.RegisterItem( 'insertHtml', oinsertHtmlItem ) ;

