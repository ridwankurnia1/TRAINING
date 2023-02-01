import { Directive, TemplateRef } from '@angular/core';

// this directive are used to load dynamic components.
// it tells the angular to use this directive as anchor point for
// rendering/insert components
@Directive({
  selector: '[appTemplate]'
})
export class AppTemplateDirective {

  constructor(public templateRef : TemplateRef<any>) { }

}
