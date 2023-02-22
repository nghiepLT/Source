/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = "Include/ObjCK/ckfinder/ckfinder.html";

    config.filebrowserImageBrowseUrl = "Include/ObjCK/ckfinder/ckfinder.html?type=Images";
    config.filebrowserImageUploadUrl = "Include/ObjCK/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";

//    config.filebrowserFlashBrowseUrl = "Include/ObjCK/ckfinder/ckfinder.html?type=Flash";
//    config.filebrowserFlashUploadUrl = "Include/ObjCK/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";

//    config.filebrowserFilesBrowseUrl = "Include/ObjCK/ckfinder/ckfinder.html?type=Files";
//    config.filebrowserFilesUploadUrl = "Include/ObjCK/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";


    config.toolbar = "Full";
};
