import { Component, OnInit } from '@angular/core';
import { Category } from '../home/category.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { HomeService } from '../home/home.service';
import { UserService } from '../shared/services/user.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {
  private categorySubscription: Subscription;
  categories: Category[];
  addCategoryForm: FormGroup;
  editCategoryForm: FormGroup;
  categoryId: number;
  delCateg: number;
  constructor(private route: ActivatedRoute, private categoryService: HomeService, private userService: UserService) { }

  ngOnInit() {
    this.categorySubscription = this.categoryService.getAllCategories().subscribe(
      (category: Category[]) => { this.categories = category; });
    this.userService.show();
    this.initForm();
  }
  initForm() {
    this.addCategoryForm = new FormGroup({
      'title': new FormControl(''),
      'description': new FormControl('')
    });
    this.editCategoryForm = new FormGroup({
      'title': new FormControl(''),
      'description': new FormControl('')
    })
  }
  addCategory() {
    this.userService.addCategory(this.addCategoryForm.controls['title'].value, this.addCategoryForm.controls['description'].value)
      .subscribe();
    location.reload();
  }
  prepCategEdit(category: Category) {
    this.editCategoryForm.patchValue({
      'title': category.title,
      'description':category.description
    });
    this.categoryId = category.id;
  }
  editCategory() {
    this.userService.editCategory(this.categoryId, this.editCategoryForm.controls['title'].value,this.editCategoryForm.controls['description'].value,)
      .subscribe(
        () => {
          // this.router.navigateByUrl('/')
          location.reload();
        }
      );
  }
  prepareDelete(category: Category) {
    this.delCateg = category.id;
  }
  deleteCategory() {
    this.userService.deleteCategory(this.delCateg).subscribe(
      //() => {
      //location.reload();
      //}
    );
  }
}
