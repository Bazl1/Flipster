# Flipster

## Microservices & Features

### Flipster.IdentityService

`[Visitor]`

- Register(email, name, password): создание нового пользователя. как результат возвращает пользователя, токен доступа и токен обновления.
- Login(email, password): логин пользователя. как результат возвращает пользователя, токен доступа и токен обновления.
- RefreshToken(): обновление токена доступа. возвращает пару новых токенов обновления и доступа.

Security layer

`[User]`

- ChangeProfile({ name, avatar }): изменяет информация об аккаунте напр. имя или аватарку. при загрузке аватара происходит обращение на сторонний сервис `Flipster.ImageService` для получения ссылки на картинку и сохранение изменений в БД.
- ChangePassword({ oldPassword, password }): изменяет пароль. как результат возвращает позователя.
- GetUserInfo(): получение информации о текущем пользователе.
- DeleteUser(): удаление текущего пользователя. статус аккаунта пользователя изменяеться на удален.

`[Admin]`

- GetUsers(): получение списка всех пользвателей.
- GetUserById(id): получение пользователя по уникальному идентификатору.
- DeleteUser(id): удаление пользователя. статус аккаунта пользователя изменяеться на удален.
- BlockUser(id): блокировка пользователя. статус аккаунта пользователя изменяеться на заблокирован.
- ChangeUser({ id, name, email, password }): изменяет данные пользователя.

### Flipster.AdvertService

`[Visitor]`

- GetAdverts()

Security layer

`[User]`

- CreateAdvert()
- DeleteAdvert()
- ChangeAdvert()
- GetAdvertById()
- ActivateAdvert()
- DeactivateAdvert()

`[Admin]`

- GetAdvertById()
- DeleteAdvert()
- ChangeAdvert()
- BlockAdvert()
- ActivateAdvert()
- DeactivateAdvert()

### Flipster.OrderService

Security layer

`[User]`

- GetOrderHistory()
- GetOrderById()

`Just for the customer`

- CreateOrder()
- CancelOrder()

`Just for the seller`

- ConfirmOrder()
- CancelOrder()

`[Admin]`

- CancelOrder()
- ConfirmOrder()
- ChangeOrder()
- GetOrderById()
- GetUserOrderHistoryById()

### Flipster.ReviewService

`[Visitor]`

- GetUserReviewsById()
- GetUserRating()

Security layer

`[User]`

- CreateReview()
- DeleteReview()
- ChangeReview()

`[Admin]`

- DeleteReview()
- ChangeReview()

### Flipster.ImageService

`[Visitor]`

- LoadImage()
- LoadImages()
- DeleteImage()
- DeleteImages()
