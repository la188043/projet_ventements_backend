# Ventements

## Api Routes

---

## _User_

| Utility            | Method | Route                                         | Role Required |
| ------------------ | ------ | --------------------------------------------- | ------------- |
| Get all users      | GET    | https://localhost:5001/api/users              | Admin         |
| Sign up            | POST   | https://localhost:5001/api/users              | None          |
| Log in             | POST   | https://localhost:5001/api/users/authenticate | None          |
| Get user by its id | GET    | https://localhost:5001/api/users/{id}         | User          |
| Add address        | POST   | https://localhost:5001/api/users/{id}/address | User          |
| Get user address   | GET    | https://localhost:5001/api/users/{id}/address | User          |

---

# _Category_

| Utility                          | Method | Route                                                    | Role Required |
| -------------------------------- | ------ | -------------------------------------------------------- | ------------- |
| Get categories                   | GET    | https://localhost:5001/api/categories                    | None          |
| Add category                     | POST   | https://localhost:5001/api/categories                    | Admin         |
| Update category                  | PUT    | https://localhost:5001/api/categories/{id}               | Admin         |
| Add a subcategory                | POST   | https://localhost:5001/api/categories/{id}/subcategories | Admin         |
| Get all category's subcategories | GET    | https://localhost:5001/api/categories/{id}/subcategories | None          |
