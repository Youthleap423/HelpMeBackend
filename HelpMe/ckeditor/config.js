/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.contentsCss = '/CKEditor/CustomFonts/fonts.css';
    config.font_names = 'LMG-Arun/LMG-Arun;' + config.font_names;
    config.font_names = 'LMG-Rupen/LMG-Rupen;' + config.font_names;

    config.toolbar = [
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'editing', items: ['Scayt'] },
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
        { name: 'styles', items: ['Font', 'FontSize', 'Format', ] }
    ];
};
