function PointerEventsPolyfill(options){this.options={selector:'*',mouseEvents:['click','dblclick','mousedown','mouseup'],usePolyfillIf:function(){if(navigator.appName=='Microsoft Internet Explorer')
{var agent=navigator.userAgent;if(agent.match(/MSIE ([0-9]{1,}[\.0-9]{0,})/)!=null){var version=parseFloat(RegExp.$1);if(version<11)
return true;}}
return false;}};if(options){var obj=this;$.each(options,function(k,v){obj.options[k]=v;});}
if(this.options.usePolyfillIf())
this.register_mouse_events();}
PointerEventsPolyfill.initialize=function(options){if(PointerEventsPolyfill.singleton==null)
PointerEventsPolyfill.singleton=new PointerEventsPolyfill(options);return PointerEventsPolyfill.singleton;};PointerEventsPolyfill.prototype.register_mouse_events=function(){$(document).on(this.options.mouseEvents.join(" "),this.options.selector,function(e){if($(this).css('pointer-events')=='none'){var origDisplayAttribute=$(this).css('display');$(this).css('display','none');var underneathElem=document.elementFromPoint(e.clientX,e.clientY);if(origDisplayAttribute)
$(this).css('display',origDisplayAttribute);else
$(this).css('display','');e.target=underneathElem;$(underneathElem).trigger(e);return false;}
return true;});};jQuery(document).ready(function(){PointerEventsPolyfill.initialize({});});