/* Copyright (c) Business Objects 2006. All rights reserved. */

if (typeof bobj.crv.PrintUI == 'undefined') {
    bobj.crv.PrintUI = {};
}

if (typeof bobj.crv.ExportUI == 'undefined') {
    bobj.crv.ExportUI = {};
}

if (typeof bobj.crv.ErrorDialog == 'undefined') {
    bobj.crv.ErrorDialog = {};
}

if (typeof bobj.crv.ReportProcessingUI == 'undefined') {
    bobj.crv.ReportProcessingUI = {};
}

/*
================================================================================
PrintUI
================================================================================
*/

bobj.crv.newPrintUI = function(kwArgs) {
    if (!kwArgs.id) {
        kwArgs = MochiKit.Base.update({id: bobj.uniqueId()}, kwArgs);
    }
    
    var lbl = kwArgs.submitBtnLabel;
    if (!lbl) {
        lbl = L_bobj_crv_submitBtnLbl;
    }
    
    var infoTitle = kwArgs.infoTitle;
    if (!infoTitle) {
        infoTitle = L_bobj_crv_PrintInfoTitle;
    }
    
    var dialogTitle = kwArgs.dialogTitle;
    if (!dialogTitle) {
        if (kwArgs.isActxPrinting) {
            dialogTitle = L_bobj_crv_ActiveXPrintDialogTitle;
        }
        else {
            dialogTitle = L_bobj_crv_PDFPrintDialogTitle;
        }
    }
    
    var infoMsg = kwArgs.infoMsg;
    if (!infoMsg) {
        infoMsg = L_bobj_crv_PrintInfo1;
        infoMsg += '\n';
        infoMsg += L_bobj_crv_PrintInfo2;
    }
    
    var o = newDialogBoxWidget(kwArgs.id + '_dialog', 
                                dialogTitle, 
                                250, 
                                100,
                                null,
                                bobj.crv.PrintUI._cancel,
                                false);
    
    o.actxId = o.id + '_actx';
    o.actxContainerId = o.id + '_actxdiv';
    o._initOld = o.init;
    o._showOld = o.show;
    
    if (!kwArgs.isActxPrinting) {
        o._fromBox = newIntFieldWidget(o.id + "_fromBox", 
                                        null, 
                                        null, 
                                        function() {o._rangeRadio.check(true);}, 
                                        null, 
                                        true,
                                        '', 
                                        50);
                                        
        o._toBox = newIntFieldWidget(o.id + "_toBox", 
                                        null, 
                                        null, 
                                        function() {o._rangeRadio.check(true);}, 
                                        null, 
                                        true,
                                        '', 
                                        50);
                                        
        o._submitBtn = newButtonWidget(o.id + "_submitBtn", 
                                        lbl, 
                                        MochiKit.Base.bind(bobj.crv.PrintUI._submitBtnCB, o));
                                        
        o._allRadio = newRadioWidget(o.id + "_allRadio", 
                                        o.id + "_grp", 
                                        L_bobj_crv_PrintAllLbl);
                                        
        o._rangeRadio = newRadioWidget(o.id + "_rangeRadio", 
                                        o.id + "_grp", 
                                        L_bobj_crv_PrintPagesLbl);
                                        
        o._optionsFrame = newFrameZoneWidget(o.id + "_optionsFrame", 250);
        
        if (!kwArgs.isExporting) {
            o._info = newInfoWidget(o.id + "_info",
                                    infoTitle, 
                                    null, 
                                    infoMsg, 
                                    100 );
        }
    }
    
    o.widgetType = 'PrintUI';
    
    // Update instance with constructor arguments
    bobj.fillIn(o, kwArgs);
    
    // Update instance with member functions
    MochiKit.Base.update(o, bobj.crv.PrintUI);
    
    return o;
};

bobj.crv.PrintUI._submitBtnCB = function() {
    var start = null;
    var end = null;
    if (this._rangeRadio.isChecked()) {
        start = parseInt(this._fromBox.getValue(), 10);
        end = parseInt(this._toBox.getValue(), 10);
        
        if (!start || !end || (start < 0) || (start > end)) {
            alert(L_bobj_crv_PrintPageRangeError);
            return;
        }
    }
    
    if (this.widgetType == 'PrintUI') {
        MochiKit.Signal.signal(this, 'printSubmitted', start, end);
    }
    else {
        MochiKit.Signal.signal(this, 'exportSubmitted', start, end, this._comboBox.getSelection().value);
    }
    
    this.show(false);
};

bobj.crv.PrintUI._getRPSafeCodeBase = function(codeBase) {
    if (!codeBase) {
        return;
    }
    
    if (codeBase.indexOf('/') === 0) {
        return codeBase;
    }
    
    var winLoc = window.location.href;
    var lPos = winLoc.lastIndexOf('/');
    
    if (lPos < 0) {
        return codeBase;
    }
    
    winLoc = winLoc.substring(0, lPos);
    return winLoc + '/' + codeBase;
};

bobj.crv.PrintUI._getObjectTag = function(postData) {
    var objectTagArr = [];
    objectTagArr.push('<OBJECT ID="');
    objectTagArr.push(this.actxId);
    objectTagArr.push('" CLASSID="CLSID:');
    objectTagArr.push(bobj.crv.ActxPrintControl_CLSID);
    objectTagArr.push('" CODEBASE="');
    objectTagArr.push(this._getRPSafeCodeBase(this.codeBase));
    objectTagArr.push('#Version=');
    objectTagArr.push(bobj.crv.ActxPrintControl_Version);
    objectTagArr.push('" VIEWASTEXT>');
    
    objectTagArr.push('<PARAM NAME="PostBackData" VALUE="');
    objectTagArr.push(postData);
    objectTagArr.push('">');
    
    objectTagArr.push('<PARAM NAME="ServerResourceVersion" VALUE="');
    objectTagArr.push(bobj.crv.ActxPrintControl_Version);
    objectTagArr.push('">');
    
    if (this.lcid) {
        objectTagArr.push('<PARAM NAME="LocaleID" VALUE="');
        objectTagArr.push(this.lcid);
        objectTagArr.push('">');
    }
    
    if (this.url) {
        objectTagArr.push('<PARAM NAME="URL" VALUE="');
        objectTagArr.push(this.url);
        objectTagArr.push('">');
    }
    
    if (this.title) {
        objectTagArr.push('<PARAM NAME="Title" VALUE="');
        objectTagArr.push(this.title);
        objectTagArr.push('">');
    }
    
    if (this.maxPage) {
        objectTagArr.push('<PARAM NAME="MaxPageNumber" VALUE="');
        objectTagArr.push(this.maxPage);
        objectTagArr.push('">');
    }
    
    if (this.paperOrientation) {
        objectTagArr.push('<PARAM NAME="PageOrientation" VALUE="');
        objectTagArr.push(this.paperOrientation);
        objectTagArr.push('">');
    }
    
    if (this.paperSize) {
        objectTagArr.push('<PARAM NAME="PaperSize" VALUE="');
        objectTagArr.push(this.paperSize);
        objectTagArr.push('">');
    }
    
    if (this.paperWidth) {
        objectTagArr.push('<PARAM NAME="PaperWidth" VALUE="');
        objectTagArr.push(this.paperWidth);
        objectTagArr.push('">');
    }
    
    if (this.paperLength) {
        objectTagArr.push('<PARAM NAME="PaperLength" VALUE="');
        objectTagArr.push(this.paperLength);
        objectTagArr.push('">');
    }
    
    if (this.driverName) {
        objectTagArr.push('<PARAM NAME="PrinterDriverName" VALUE="');
        objectTagArr.push(this.driverName);
        objectTagArr.push('">');
    }
    
    if (this.useDefPrinter) {
        objectTagArr.push('<PARAM NAME="UseDefaultPrinter" VALUE="');
        objectTagArr.push(this.useDefPrinter);
        objectTagArr.push('">');
    }
    
    if (this.useDefPrinterSettings) {
        objectTagArr.push('<PARAM NAME="UseDefaultPrinterSettings" VALUE="');
        objectTagArr.push(this.useDefPrinterSettings);
        objectTagArr.push('">');
    }
    
    if (this.sendPostDataOnce) {
        objectTagArr.push('<PARAM NAME="SendPostDataOnce" VALUE="');
        objectTagArr.push(this.sendPostDataOnce);
        objectTagArr.push('">');
    }
    
    objectTagArr.push('</OBJECT>');
    
    return objectTagArr.join('');
};

bobj.crv.PrintUI._cancel = function() {
    if (this.isActxPrinting) {
        document.getElementById(this.actxContainerId).innerHTML = '';
    }
};

bobj.crv.PrintUI.show = function(visible, postBackData) {
    if (visible) {
        if (!this.layer) {
            targetApp(this.getHTML());
            this.init();
        }
        if (this.isActxPrinting) {
            document.getElementById(this.actxContainerId).innerHTML = this._getObjectTag(postBackData);
        }
        this._showOld(true);
    } 
    else if (this.layer) {
        this._showOld(false);
    }
};

bobj.crv.PrintUI.init = function() {
    this._initOld();
    
    if (!this.isActxPrinting) {
        this._fromBox.init();
        this._toBox.init();
        this._submitBtn.init();
            
        this._optionsFrame.init();
        this._allRadio.init();
        this._rangeRadio.init();
        
        if (!this.isExporting) {
            this._info.init();
        }
                    
        this._allRadio.check(true);
        
        if (this.widgetType == 'ExportUI') {
            this._initExportList();
        }
    }
};

bobj.crv.PrintUI.getHTML = function(){
    var h = bobj.html;
        
    var o = this;
    var strArr = [];
    
    strArr.push( o.beginHTML());
    
    if (!this.isActxPrinting) {
        strArr.push( '<table class="dialogzone" border=0 cellpadding=0 cellspacing=2>');
        strArr.push( '<tr>');
        strArr.push( '  <td style="padding-top:4px;">');
        
        strArr.push(      o._optionsFrame.beginHTML());
        
        if (this.widgetType == 'ExportUI') {
            strArr.push( this._getExportList());
        }
        
        strArr.push(      L_bobj_crv_PrintRangeLbl);
        strArr.push(      o._allRadio.getHTML());
        strArr.push(      o._rangeRadio.getHTML());
        strArr.push(      '<span style="padding-left:20px">' + L_bobj_crv_PrintFromLbl + '</span>');
        strArr.push(      o._fromBox.getHTML());
        strArr.push(      '<span style="padding-left:4px">' + L_bobj_crv_PrintToLbl + '</span>');
        strArr.push(      o._toBox.getHTML());
        strArr.push(      o._optionsFrame.endHTML());
        
        strArr.push( '  </td>');
        strArr.push( '</tr><tr>');
        
        if (!this.isExporting) {
            strArr.push( '  <td style="padding-top:8px;">');
            strArr.push(      o._info.getHTML());
            strArr.push( '  </td>');
            strArr.push( '</tr><tr>');
        }
        
        strArr.push( '  <td align="right" style="padding-top:4px;">');
        strArr.push(      o._submitBtn.getHTML());
        strArr.push( '  </td>');
        strArr.push( '</tr></table>');
    }
    else {
        strArr.push( h.DIV({id:this.actxContainerId}));
        strArr.push( '<script for="' + this.actxId + '" EVENT="Finished(status, statusText)" language="javascript">');
        strArr.push( 'getWidgetFromID("' + this.id + '").show(false);');
        strArr.push( '</script>');
    }
    
    strArr.push( o.endHTML());
    strArr.push( bobj.crv.getInitHTML(this.widx));
    
    return strArr.join('');
};

/*
================================================================================
ExportUI
================================================================================
*/
bobj.crv.newExportUI = function(kwArgs) {
    kwArgs = MochiKit.Base.update({ submitBtnLabel:L_bobj_crv_ExportBtnLbl,
                                    dialogTitle:L_bobj_crv_ExportDialogTitle,
                                    infoTitle:L_bobj_crv_ExportInfoTitle,
                                    infoMsg:L_bobj_crv_PrintInfo1,
                                    isExporting:true}, kwArgs);
    
    var o = bobj.crv.newPrintUI(kwArgs);
    
    o._comboBox = newCustomCombo(
        o.id + "_combo",
        MochiKit.Base.bind(bobj.crv.ExportUI._onSelectFormat, o),
        false,
        270,
        null);
        
    o.availableFormats = (o.availableFormats ? eval(o.availableFormats) : null);
    var itemsCount = (bobj.isArray(o.availableFormats) ? o.availableFormats.length : 0);
    
    for (var i = 0; i < itemsCount; i++) {
        var item = o.availableFormats[i];
        o._comboBox.add(item.name, item.value, item.isSelected);
    }
    
    o.widgetType = 'ExportUI';
    
    MochiKit.Base.update(o, bobj.crv.ExportUI);
    
    return o;
};

bobj.crv.ExportUI._onSelectFormat = function() {
    var format = this._comboBox.getSelection().value;
    if (format == 'CrystalReports' || format == 'RecordToMSExcel' || format == 'CharacterSeparatedValues' || format == 'XML') {
        this._fromBox.setDisabled(true);
        this._toBox.setDisabled(true);
        
        this._rangeRadio.check(false);
        this._rangeRadio.setDisabled(true);
        
        this._allRadio.check(true);
    }
    else {
        this._fromBox.setDisabled(false);
        this._toBox.setDisabled(false);
        this._rangeRadio.setDisabled(false);
    }
};

bobj.crv.ExportUI._initExportList = function() {
    this._comboBox.init();
    this._onSelectFormat();
};

bobj.crv.ExportUI._getExportList = function() {
    return L_bobj_crv_ExportFormatLbl + this._comboBox.getHTML();
};

/*
================================================================================
ErrorDialog

TODO Dave: If time permits, make dialog resizable with mouse
================================================================================
*/

/**
 * Static function.
 * @returns [ErrorDialog] Returns a shared Error Dialog
 */
bobj.crv.ErrorDialog.getInstance = function() {
    if (!bobj.crv.ErrorDialog.__instance) {
        bobj.crv.ErrorDialog.__instance = bobj.crv.newErrorDialog();
    }
    return bobj.crv.ErrorDialog.__instance;
};

bobj.crv.newErrorDialog = function(kwArgs) {
    kwArgs = MochiKit.Base.update({ 
        id: bobj.uniqueId(),
        title: L_bobj_crv_Error,
        text: null,
        detailText: null,
        okLabel: L_bobj_crv_OK,
        promptType: _promptDlgCritical
    }, kwArgs);
    
    var o = newPromptDialog(
        kwArgs.id,
        kwArgs.title,
        kwArgs.text,
        kwArgs.okLabel,
        null,   // cancelLabel
        kwArgs.promptType,
        null,   // yesCB
        null,   // noCB,
        true); // noCloseButton
    
    o.widgetType = 'ErrorDialog';
    
    // Update instance with constructor arguments
    bobj.fillIn(o, kwArgs);

    // Update instance with member functions
    o._promptDlgInit = o.init;
    o._promptDialogSetText = o.setText;
    o._promptDialogShow = o.show;
    o._promptDialogSetTitle = o.setTitle;
    o._promptDialogSetPromptType = o.setPromptType;
    MochiKit.Base.update(o, bobj.crv.ErrorDialog);
    
    o.noCB = MochiKit.Base.bind(o._onClose, o);
    o.yesCB = o.noCB;
    
    o._detailBtn = newIconWidget(
        o.id + "_detailBtn", 
        bobj.skinUri('../help.gif'), 
        MochiKit.Base.bind(bobj.crv.ErrorDialog._onDetailBtnClick, o),  
        L_bobj_crv_showDetails, // Text
        null, // Tooltip 
        16,16,0,0,22,0);
        
    return o;
};

bobj.crv.ErrorDialog.init = function() {
    this._promptDlgInit();
    this._detailBtn.init(); 
    this._detailRow = document.getElementById(this.id + '_detRow');
    this._detailArea = document.getElementById(this.id + '_detArea'); 
    
    if (!this.detailText) {
        this._detailBtn.show(false);    
    }
};

bobj.crv.ErrorDialog.getHTML = function() {
    var TABLE = bobj.html.TABLE;
    var TBODY = bobj.html.TBODY; 
    var TR = bobj.html.TR;
    var TD = bobj.html.TD;
    var PRE = bobj.html.PRE;
    var DIV = bobj.html.DIV;
    
    var imgPath = PromptDialog_getimgPath(this.promptType);
    var imgAlt = PromptDialog_getimgAlt(this.promptType);		
    
    var width = "320";	
    var detailWidth = "300px"; 
    var detailHeight = "100px";
    
    var contentHTML = 
        TABLE({'class':"dialogzone", width: width, cellpadding:"0", cellspacing:"5", border:"0"},
            TBODY(null,
                TR(null, TD(null,
                    TABLE({'class':"dialogzone", cellpadding:"5", cellspacing:"0", border:"0"},                                       
                        TBODY(null,
                            TR(null, 
                                TD({align:"right", width:"32"}, 
                                    img(imgPath, 32, 32, null, 'id="dlg_img_' + this.id + '"', imgAlt)),
                                TD(),
                                TD({id:"dlg_txt_" + this.id, align:"left", tabindex:"0"},
                                    convStr(this.text, false, true))))))),
                TR({id: this.id + '_detRow', style: {display: "none"}}, 
                    TD(null, DIV({'class': "infozone", style: {width: detailWidth, 'height': detailHeight, overflow: "auto"}},
                        PRE({id: this.id + '_detArea'}, this.detailText)))),                                    
                TR(null, TD(null, getSep())),
                TR(null, TD(null,
                    TABLE({cellpadding:"5", cellspacing:"0", border:"0", width:"100%"},
                        TBODY(null, 
                            TR(null,
                                TD({align:"left"}, this._detailBtn.getHTML()),
                                TD({align:"right"}, this.yes.getHTML())))))))); 
                    
            
    return this.beginHTML() + contentHTML + this.endHTML();
};

/**
 * Set the error message and detail text.
 *
 * @param text       [String] Error message
 * @param detailText [String] Detailed error message or technical info
 */
bobj.crv.ErrorDialog.setText = function (text, detailText) {
    this.text = text;
    this.detailText = detailText;
    
    if (this.layer) {
        this._promptDialogSetText(text || '');
        
        if (this._detailArea) {
            this._detailArea.innerHTML = detailText || '';
        }
        
        var showDetails = detailText ? true : false;
        this._detailBtn.show(showDetails);
        if (!showDetails) {
            this.showDetails(false);    
        }
    }
};

bobj.crv.ErrorDialog.setTitle = function (title) {
    this.title = title;
    if (this.layer) {
        this._promptDialogSetTitle(title || '');
    }
};

bobj.crv.ErrorDialog.setPromptType = function (promptType) {
    this.promptType = promptType;
    if (this.layer) {
        this._promptDialogSetPromptType(promptType);
    }
};

/**
 * Show/Hide the dialog
 *
 * @param isShow  [bool(=true)]  True value displays the dialog, false hides it.
 * @param closeCB [function]     Callback to call after the next close event
 */
bobj.crv.ErrorDialog.show = function(isShow, closeCB) {
    if (typeof isShow == 'undefined') {
        isShow = true;
    }
    
    if (isShow) {
        this._closeCB = closeCB;
        if (!this.layer) {
            targetApp(this.getHTML());
            this.init();
        }
        this.layer.onkeyup = DialogBoxWidget_keypress;
        DialogBoxWidget_keypress = MochiKit.Base.noop;
        
        this._promptDialogShow(true);
    } 
    else if (this.layer){
        this._closeCB = null;
        this._promptDialogShow(false);
    }
};

/**
 * Show/Hide the detailed error message
 *
 * @param isShow [bool(=true)]  True value displays the details, false hides them.
 */
bobj.crv.ErrorDialog.showDetails = function(isShow) {
    if (typeof isShow == 'undefined') {
        isShow = true;
    }
    
    if (this._detailRow && this._detailBtn) {
        if (isShow) {
            this._detailRow.style.display = '';
            this._detailBtn.changeText(L_bobj_crv_hideDetails);
        }
        else {
            this._detailRow.style.display = 'none';
            this._detailBtn.changeText(L_bobj_crv_showDetails);
        }
    }
};

/**
 * Private. Handles detail button clicks.
 */
bobj.crv.ErrorDialog._onDetailBtnClick = function() { 
    if (this._detailRow) {
        this.showDetails(this._detailRow.style.display == 'none');
    }
};

/**
 * Private. Notifies listener that dialog has closed;
 */
bobj.crv.ErrorDialog._onClose = function() {
    if (this._closeCB) {
        this._closeCB();
        this._closeCB = null;
    }
    DialogBoxWidget_keypress = this.layer.onkeyup;
    this.layer.onkeyup = null;
};

/*
================================================================================
Report Processing Dialog
================================================================================
kwArgs
delay   - the wait time prior to showing the dialog.
message - a customized message to display in the dialog.
*/
bobj.crv.newReportProcessingUI = function(kwArgs) {
    kwArgs = MochiKit.Base.update({
        id: bobj.uniqueId(),
        delay: 3000,
        message: L_bobj_crv_ReportProcessingMessage
    }, kwArgs);
    
    /* Since JSON escapes the '\' in unicode character references (\uXXXX) in Viewer
     * process indicator text is converted to html numeric referece (&#ddddd) which Javascript
     * don't display as expected. Little hack here to it as HTML string */
    var d = document.createElement('div');
    d.style.visibility = 'hidden';
    d.innerHTML = kwArgs.message;
    var newMsg = d.innerHTML;
    d = null;
    
    var o = newWaitDialogBoxWidget(
        kwArgs.id,          // id
        0,                  // width
        0,                  // height
        '',                 // title
        true,               // show cancel
        bobj.crv.ReportProcessingUI.cancelCB,               // cancel callback
        false,              // show UnDeterminate progress
        null,               // fake duration.  10s
        true,               // show label
        newMsg,	            // label text 
        false               // show Determinate
        );				
    
    
    o.widgetType = 'ReportProcessingUI';
    o.delay = kwArgs.delay;
    
    // Update instance with member functions
    MochiKit.Base.update(o, bobj.crv.ReportProcessingUI);
    
    return o;
};

bobj.crv.reportProcessingDialog = null;
bobj.crv.timerID = null;

bobj.crv.ReportProcessingUI.cancelCB = function ()
{
    bobj.crv.reportProcessingDialog.cancelled = true;

    if (bobj.crv.reportProcessingDialog.deferred !== null) {
        bobj.crv.reportProcessingDialog.deferred.cancel ();
    }

    bobj.crv.reportProcessingDialog.cancelShow ();
};

bobj.crv.ReportProcessingUI.wasCancelled = function ()
{
    return bobj.crv.reportProcessingDialog.cancelled;
};

bobj.crv.ReportProcessingUI.delayedShow = function (showCancel) {
    // cleanup any existing dialog?
    if (bobj.crv.reportProcessingDialog !== null) {
        bobj.crv.reportProcessingDialog.cancelShow ();
    }
    
    if (!this.layer) {
        targetApp(this.getHTML());
        this.init();
    }

    this.cancelled = false;
    this.deferred = null;
    this.setShowCancel (showCancel, showCancel ? this.cancelCB : null);
    bobj.crv.reportProcessingDialog = this;
    bobj.crv.timerID = setTimeout("bobj.crv._showReportProcessingDialog ()", bobj.crv.reportProcessingDialog.delay);
};
    
bobj.crv.ReportProcessingUI.cancelShow = function () {
    if (bobj.crv.timerID) {
        clearTimeout (bobj.crv.timerID);
    }
            
    if (bobj.crv.reportProcessingDialog){
        bobj.crv.reportProcessingDialog.show (false);
    }
    
    bobj.crv.reportProcessingDialog = null;
    bobj.crv.timerID = null;
};

bobj.crv.ReportProcessingUI.setDeferred = function (deferred) {
    bobj.crv.reportProcessingDialog.deferred = deferred;
    
    if (bobj.crv.reportProcessingDialog.wasCancelled () === true) {
        deferred.cancel ();
    }
};

bobj.crv._showReportProcessingDialog = function () {
    if (bobj.crv.reportProcessingDialog && bobj.crv.reportProcessingDialog.delay !== 0) {
        bobj.crv.reportProcessingDialog.show (true);
    }
};