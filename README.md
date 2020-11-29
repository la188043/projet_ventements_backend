# Ventements

## Api Routes

---

# _User_

| Utility        | Method | Route                                         | Role Required |
| -------------- | ------ | --------------------------------------------- | ------------- |
| Get all users  | GET    | https://localhost:5001/api/users              | Admin         |
| Get user by id | GET    | https://localhost:5001/api/users/{userId}     | User          |
| Sign up        | POST   | https://localhost:5001/api/users              | None          |
| Log in         | POST   | https://localhost:5001/api/users/authenticate | None          |

---

# _Address_

| Utility             | Method | Route                                             | Role Required |
| ------------------- | ------ | ------------------------------------------------- | ------------- |
| Add address to user | POST   | https://localhost:5001/api/users/{userId}/address | User          |

# _Category_

| Utility            | Method | Route                                                            | Role Required |
| ------------------ | ------ | ---------------------------------------------------------------- | ------------- |
| Get categories     | GET    | https://localhost:5001/api/categories                            | None          |
| Get category by id | GET    | https://localhost:5001/api/categories/{categoryId}/subcategories | None          |
| Add category       | POST   | https://localhost:5001/api/categories                            | Admin         |
| Add a subcategory  | POST   | https://localhost:5001/api/categories/{categoryId}/subcategories | Admin         |

---

# _Item_

| Utility         | Method | Route                                                    | Role Required |
| --------------- | ------ | -------------------------------------------------------- | ------------- |
| Get by id       | GET    | https://localhost:5001/api/items/{itemId}                | None          |
| Get by category | GET    | https://localhost:5001/api/categories/{categoryId}/items | None          |
| Add item        | POST   | https://localhost:5001/api/categories/{categoryId}/items | Admin         |
| Update item     | PUT    | https://localhost:5001/api/items/{itemId}                | Admin         |

---

# _Review_

| Utility     | Method | Route                                                     | Role Required |
| ----------- | ------ | --------------------------------------------------------- | ------------- |
| Get all     | GET    | https://localhost:5001/api/reviews/                       | None          |
| Get by item | GET    | https://localhost:5001/api/items/{itemId}/reviews         | None          |
| Add         | POST   | https://localhost:5001/api/reviews/{userId}/item/{itemId} | User          |
| Update      | PUT    | https://localhost:5001/api/reviews/{reviewId}             | User          |
| Delete      | DEL    | https://localhost:5001/api/reviews/{reviewId}             | Admin         |

---

# _BaggedItem_

| Utility                   | Method | Route                                                  | Role Required |
| ------------------------- | ------ | ------------------------------------------------------ | ------------- |
| Get user bag              | GET    | https://localhost:5001/api/users/{userId}/bag          | User          |
| Add item to user bag      | POST   | https://localhost:5001/api/users/{userId}/bag/{itemId} | User          |
| Update item quantity      | PUT    | https://localhost:5001/api/baggedItems/{baggedItemId}  | User          |
| Delete item from user bag | DEL    | https://localhost:5001/api/baggedItems/{baggedItemId}  | User          |
| Empty user bag            | DEL    | https://localhost:5001/api/users/{userId}/bag/empty    | User          |
