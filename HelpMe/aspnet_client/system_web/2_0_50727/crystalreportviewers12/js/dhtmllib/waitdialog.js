// <script>

/*
=============================================================
WebIntelligence(r) Report Panel
Copyright(c) 2001-2003 Business Objects S.A.
All rights reserved

Use and support of this software is governed by the terms
and conditions of the software license agreement and support
policy of Business Objects S.A. and/or its subsidiaries.
The Business Objects products and technology are protected
by the US patent number 5,555,403 and 6,247,008

File: waitdialog.js
Waiting Dialog boxes

dom.js dialog.js palette.js must be included before

=============================================================
*/


// ================================================================================
// ================================================================================
//
// OBJECT newDeterminateProgressBarWidget (Constructor)
//
// Class for progress bar
//
// ================================================================================
// ================================================================================

function newDeterminateProgressBarWidget(id,w)
// CONSTRUCTOR
// id			[String]	the id for DHTML processing
{
	var o=newWidget(id)

	o.gifW=w
	o.gifH=9
		
	o.oldWidgetInit=o.init
	o.init=DeterminateProgressBarWidget_init
	o.getHTML=DeterminateProgressBarWidget_getHTML
	
	return o
}

// ================================================================================

function DeterminateProgressBarWidget_init()
// Init the widget layers
// Return [void]
{
	var o=this
	o.oldWidgetInit()	
}

// ================================================================================

function DeterminateProgressBarWidget_getHTML()
// getHTML
// Returns the HTML generated for the DeterminateProgressBarWidget
{
	var o=this, s=''
	
	s+=	'<table id="' + o.id + '" border="0" cellspacing="0" cellpadding="0" width="'+o.gifW+'"><tbody>'
	s+=	'<tr height="'+ o.gifH +'">'	
	s+=		'<td align="center" width="'+ o.gifW + '">' +
				'<div style="height:'+ o.gifH +'px;width:' + o.gifW + 'px;overflow:hidden;'+ backImgOffset(_skin+'wait02.gif',0,0) +'">' +				
				'</div>' +
			'</td>'
	s+=	'</tr>'

	s+=	'</tbody></table>'
		
	return s
}

// ================================================================================
// ================================================================================
//
// OBJECT newUnDeterminateProgressBarWidget (Constructor)
//
// Class for progress bar
//
// ================================================================================
// ================================================================================

function newUnDeterminateProgressBarWidget(id,w,duration)
// CONSTRUCTOR
// id			[String]	the id for DHTML processing
// w			[int]		width of the progress bar contains
// duration		[int]		total duration of the progress bar
{
	var o=newWidget(id)
	o.duration=((duration!=null)&&(duration>0)) ? duration : 1
	o.items=new Array

	o.borderW=3
	o.gifW=8
	o.gifH=13

	// The width of the progress bar must be a multiple of the size of a square
	var nb=Math.floor((w-2*o.borderW)/o.gifW)
	o.width=nb*o.gifW+2*o.borderW
	o.oldWidgetInit=o.init
	o.init=UnDeterminateProgressBarWidget_init
	o.getHTML=UnDeterminateProgressBarWidget_getHTML
	o.begin=UnDeterminateProgressBarWidget_begin
	o.anim=UnDeterminateProgressBarWidget_anim
	o.clearProgress=UnDeterminateProgressBarWidget_clearProgress

	o.nbStep=0
	o.nCurrentStep=0
	o.interval=null

	return o
}

// ================================================================================

function UnDeterminateProgressBarWidget_init()
// Init the widget layers
// Return [void]
{
	var o=this
	o.oldWidgetInit()
}

// ================================================================================

function UnDeterminateProgressBarWidget_getHTML()
// getHTML
// Returns the HTML generated for the ProgressBarWidget
{
	var o=this, s=''

	s+=	'<table id="' + o.id + '" border="0" cellspacing="0" cellpadding="0" width="'+o.width+'"><tbody>'
	s+=	'<tr height="'+o.gifH+'">'
	s+=		'<td align="left" width="'+o.borderW+'">' +
				imgOffset(_skin+'progress.gif',3,13,5,13) +
			'</td>'
	s+=		'<td align="left" width="'+ (o.width-2*o.borderW) + '" style="'+ backImgOffset(_skin+'progress.gif',0,0) +'">' +
				'<div id="img_' + o.id + '" style="height:13px;width:0px;overflow:hidden;'+ backImgOffset(_skin+'progress.gif',0,39) +'"> ' +
				'</div>' +
			'</td>'
	s+=		'<td align="right" width="'+o.borderW+'">' +
				imgOffset(_skin+'progress.gif',3,13,0,26) +
			'</td>'
	s+=	'</tr>'	
	s+=	'</tbody></table>'

	return s
}

// ================================================================================

function UnDeterminateProgressBarWidget_clearProgress()
// Clear interval
{
	var o=this;
	if (o.interval!=null)
	{
		clearInterval(o.interval);
		o.interval=null;
	}
}

// ================================================================================

function UnDeterminateProgressBarWidget_begin()
// Begins the "animation" of the progress bar
{
	var o=this

	o.duration=((o.duration!=null)&&(o.duration>0)) ? o.duration : 1


	// We calculate what a single square represents in ms
	var step=8*o.duration/o.width
	
	o.nbStep=o.duration/step
	o.nCurrentStep=0
	o.clearProgress()
	// We set an interval for toolbar progression
	o.interval=setInterval('UnDeterminateProgressBarWidget_animation(' + o.par.widx + ')',step)

	// reset visually to zero
	var lyr=getLayer('img_'+o.id)
	if (lyr)
		lyr.style.width=0
}

// ================================================================================

function UnDeterminateProgressBarWidget_animation(idx)
// PRIVATE
{
	_widgets[idx].uprogressBar.anim()
}

// ================================================================================

function UnDeterminateProgressBarWidget_anim()
// Animate the progress bar
{

	var o=this,i=o.nCurrentStep
	getLayer('img_'+o.id).style.width=o.gifW*i
	i++;
	
	if (i>o.nbStep)
		o.clearProgress()
	else
		o.nCurrentStep=i
}

// ================================================================================
// ================================================================================
//
// OBJECT newWaitDialogBoxWidget (Constructor)
//
// Class for waiting dialog box
//
// ================================================================================
// ================================================================================

function newWaitDialogBoxWidget(id,w,h,title,bShowCancel,cancelCB,bShowUPB,
	duration,bShowLabel,text,bShowDPB)
// CONSTRUCTOR
// id				[String]            the id for DHTML processing
// title			[String]            the caption dialog text
// width			[int optional]      dialog box width
// height			[int optional]      dialog box height
// showCancel		[boolean optional]	if true, a CANCEL button is displayed
// cancelCB			[Function optional] callback when esc key is hit or CANCEL button pushed
// bShowUPB			[boolean optional]	if true, the undeterminant progress Bar is displayed
// duration			[int optional]		duration in ms
// showLabel		[boolean optional]	if true, a label is displayed
// text				[String optional]	label
// bShowDPB			[boolean optional]	if true, the determinant progress Bar is displayed
{
	// Min values for width and height of this dialog box
	var minW=250
	var minH=150
	if (w<minW) 
		w=minW
	if (h<minH) 
		h=minH
		
    // Justin: I've added the close button to the top right corner (last param=false) so that the user can hide 
    // the progress dialog.  The cancel button option of the waitdialog adds a full button to the dialog 
    // and gives the user the expectation that the request has been cancelled vs. just hiding this dialog.
	var o=newDialogBoxWidget(id,title,w,h,null,WaitDialogBoxWidget_cancelCB,false)
	
	// Properties
	o.pad=5	
	var zoneW=o.getContainerWidth()-10
	var zoneH=o.getContainerHeight()-(2*o.pad+21+10)
	o.frZone=newFrameZoneWidget(id+"_frZone",zoneW,zoneH)	
	var pbW=zoneW-o.pad
	o.uprogressBar=newUnDeterminateProgressBarWidget(id+"_uprogressBar",pbW,duration)
	o.uprogressBar.par=o
	o.dprogressBar=newDeterminateProgressBarWidget(id+"_dprogressBar",pbW)
	o.showUPB=(bShowUPB!=null)?bShowUPB:false
	o.showDPB=(bShowDPB!=null)?bShowDPB:false
	o.showLabel=(bShowLabel!=null)?bShowLabel:false	
	o.showCancel=(bShowCancel!=null)?bShowCancel:false
	o.label=newWidget(id+"_label")
	o.label.text=text
	o.cancelButton=newButtonWidget(id+"_cancelButton",_cancelButtonLab,CancelButton_cancelCB)
	o.cancelButton.par=o
	o.cancelCB=cancelCB
	
	// Methods
	o.oldDialogBoxInit=o.init
	o.init=WaitDialogBoxWidget_init
	o.oldShow2=o.show
	o.show=WaitDialogBoxWidget_show
	o.getHTML=WaitDialogBoxWidget_getHTML
	o.setShowCancel=WaitDialogBoxWidget_setShowCancel
	o.setShowUPB=WaitDialogBoxWidget_setShowUPB
	o.setShowLabel=WaitDialogBoxWidget_setShowLabel
	o.startProgress=WaitDialogBoxWidget_startProgress

	return o
}

// ================================================================================

function WaitDialogBoxWidget_init()
// Init the widget layers
// Return [void]
{
	var o=this
	o.oldDialogBoxInit()

	o.frZone.init()

	o.uprogressBar.init()
	o.uprogressBar.setDisplay(o.showUPB)
	
	o.dprogressBar.init()
	o.dprogressBar.setDisplay(o.showDPB)

	o.label.init()
	o.label.setDisplay(o.showLabel)
	
	o.cancelButton.init()
	o.cancelButton.setDisplay(o.showCancel)
}

// ================================================================================

function WaitDialogBoxWidget_getHTML()
// getHTML
// Returns the HTML generated for the WaitDialogBoxWidget
{
	var o=this, s=''

	s+=	o.beginHTML()

	s+=	'<table border="0" cellspacing="0" cellpadding="0" width="100%"><tbody>'
	s+=	'<tr>' +
			'<td align="center" valign="top">' +
				o.frZone.beginHTML()
	s+=			'<table border="0" cellspacing="0" cellpadding="0" width="100%"><tbody>'	+
				'<tr>' +
					'<td align="center" style="padding-top:5px;">' + img(_skin+'wait01.gif',200,40) + '</td>' +
				'</tr>' +
				'<tr>' +
					'<td align="center" style="padding-top:5px;">' + o.uprogressBar.getHTML() + '</td>' +
				'</tr>' +
				'<tr>' +
					'<td align="center" style="padding-top:5px;">' + o.dprogressBar.getHTML() + '</td>' +
				'</tr>' +
				'<tr>' +
					'<td align="left" style="padding-left:2px;padding-right:2px;padding-top:5px;">' +
						'<div id="'+o.label.id+'" class="icontext" style="wordWrap:break_word;">'+convStr(o.label.text,false,true)+'</div>'+
					'</td>' +
				'</tr>' +
				'</tbody></table>'
	s+=			o.frZone.endHTML() +
			'</td>' +
		'</tr>'
	s+=	'<tr>' +
			'<td align="center" valign="middle" style="padding-top:5px;">' +
				'<div id="cancelDiv' + o.id + '">' + o.cancelButton.getHTML() + '</div>' +
			'</td>' +
		'</tr>'
	s+=	'</tbody></table>'

	s+= o.endHTML()

	return s
}

// ================================================================================

function WaitDialogBoxWidget_setShowCancel(show,cancelCB)
// Show or hide the cancel button
// show	[boolean]	if true, cancel button is displayed
{
	var o=this

	o.showCancel=show
	o.cancelButton.setDisabled(false)
	o.cancelButton.setDisplay(show)	
	o.cancelCB=cancelCB
}

// ================================================================================

function WaitDialogBoxWidget_setShowUPB(show,duration)
// Show or hide the progress bar
// show		[boolean]	if true, progress bar is displayed
// duration	[int]		progress bar duration
{
	var o=this
	
	o.showUPB=show
	o.uprogressBar.setDisplay(o.showUPB)
	o.uprogressBar.duration=duration
}

// ================================================================================

function WaitDialogBoxWidget_setShowLabel(show,text)
// Show or hide the label
// show	[boolean]	if true, label is displayed
// text	[string]	text label
{
	var o=this

	o.showLabel=show
	o.label.text=text
	o.label.setHTML(text)
	o.label.setDisplay(show)
}

// ================================================================================

function WaitDialogBoxWidget_cancelCB()
// Callback called when the cancel button is hit
{
	var o=this
	if (o.cancelCB != null)
	{
		o.cancelCB()		
		o.cancelButton.setDisabled(true);
	}
}

// ================================================================================

function CancelButton_cancelCB()
// Callback called when the cancel button is hit
{
	var o=this
	if (o.par.cancelCB != null)
	{
		o.par.cancelCB()		
		o.par.cancelButton.setDisabled(true);
	}
}

// ================================================================================

function WaitDialogBoxWidget_startProgress()
// Start the progress bar animation
{
	var o=this
	o.uprogressBar.begin()
}

// ================================================================================

function WaitDialogBoxWidget_show(bShow)
// Show the WaitDialog and start the progress bar animation if necessary
// bShow	[boolean]	if true, waitDialog is displayed
{
	var o=this
	if (bShow)
	{
		if (o.showCancel)
			o.frZone.resize(null,o.getContainerHeight()-(2*o.pad+21+10))			
		else
			o.frZone.resize(null,o.getContainerHeight()-10)
			
	}
	o.oldShow2(bShow)
	if (bShow)
		o.uprogressBar.begin()
	else
	{
		if (o.uprogressBar.clearProgress)
			o.uprogressBar.clearProgress()
	}
}
