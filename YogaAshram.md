# Yoga Ashram
Веб приложение с использованием CRM системы для внутреннего использования для студии Classical Yoga Ashram<br>

## Проблемы заказчика
Сложность ведения учета в таблицах Excel<br>
Ограничение доступа к таблицам<br>
Долгий процесс оформления клиента<br>
Отслеживание работы сотрудников<br>
Отслеживание кассы и прозрачность денежных операции<br>
“Зависание”  компьютеров из за больших таблиц<br>
Сложность отслеживания посещаемости клиентов<br>
Сложность анализа  продаж<br>

## Цель проекта
Было решить все проблемы заказчика в одном системе. 

## Бизнес логика
**Условно наше приложение выполняет  3 функции**
- Ведение клиентской базы 
- Учет посещения клиентов
- Учет кассы 


## Использованный база данных 
Использовали базу данных **Postgres SQL**<br>
Для подключение PostgresSQL исползовалось библиотека **Npgsql.EntityFrameworkCore.PostgresSQL**<br>
Подключение библиотеки PostgresSQL происходить в классе Startup.cs ссылка-[link Startup.cs](./yogaAshram/Startup.cs)<br>
Подключение базы данных происходить в файле appsettings.json ссылка-[link appsettings.json](./yogaAshram/appsettings.json) строка 1-5 здесь можно по менять настройки подключения<br>

## Параметры приложения
Изпользовалось в приложения **ASP.NET Inentity** при подключения **Inentity** в него по умолчанию добавляется все необходимые файлы для работы с AspNet Identity.Inentity
Чтобы добавить в проект **ASP.NET Inentity**, нам надо добавить следующие NuGet-пакет: **Microsoft.AspNet.Identity.EntityFramework**
И так теперь добавим классы пользователей и контекста данных в папку Models каласс YogaAshramContext cсылка-[link YogaAshramContext.cs](./yogaAshram/Models/YogaAshramContext.cs).
Дальше создаем необходимые классы в папку Models классы пример **Employee, Users** мы использовали класс **Elmployee** с необходимыми полями для работника чтобы провести регистрацию и логин.
Дальше нужно будет изменить, добавим в файл Startup.cs со следующим содержанием cсылка-[link Startup.cs](./yogaAshram/Startup.cs) в методе ConfigureServices строка 40.<br>

**Структура identity**
***Пользователи***<br>
- Claims: возвращает коллекцию claims - специальных атрибутов, которыми обладает пользователь и которые хранят о пользователе определенную информацию<br>
- Email: email пользователя<br>
- Id: уникальный идентификатор пользователя<br>
- Logins: возвращает коллекцию логинов пользователя<br>
- PasswordHash: возвращает хэш пароля<br>
- Roles: возвращает коллекцию ролей пользователя<br>
- PhoneNumber: возвращает номер телефона<br>
- SecurityStamp: возвращает некоторое значение, которое меняется при каждой смене настроек аутентификации для данного пользователя<br>
- UserName: возвращает ник пользователя<br>
- AccessFailedCount: число попыток неудачного входа в систему<br>
- EmailConfirmed: возвращает true, если email был подтвержден<br>
- PhoneNumberConfirmed: возвращает true, если телефонный номер был подтвержден<br>
- TwoFactorEnabled: если равен true, то для данного пользователя включена двухфакторная авторизация<br>

***Менеджер пользователей UserManager***<br>
Этот класс определяет множество методов, которые имеют как синхронные, так и асинхронные версии.
- ChangePassword(id, old, new) / ChangePasswordAsync(id, old, new): изменяет пароль пользователя<br>
- Create(user) / CreateAsync(user): создает нового пользователя<br>
- Delete(user) / DeleteAsync(user): удаляет пользователя<br>
- Find(user, pass) / FindAsync(user, pass): ищет пользователя по определенному логину и паролю<br>
- FindById(id) /FindByIdAsync(id): ищет пользователя по id<br>
- FindByEmail(email) / FindByEmailAsync(email): ищет пользователя по email<br>
- FindByName(name) / FindByNameAsync(name): ищет пользователя по нику<br>
- Update(user) / UpdateAsync(user): обновляет пользователя<br>
- Users: возвращает всех пользователей<br>
- AddToRole(id, name) / AddToRoleAsync(id, name): добавляет для пользователя с определенным id роль name<br>
- GetRoles(id) / GetRolesAsync (id): получает все роли пользователя по id<br>
- IsInRole(id, name) / IsInRoleAsync(id, name): возвращает true, если пользователь с данным id имеет роль name<br>
- RemoveFromRole(id, name) / RemoveFromRoleAsync(id, name): удаляет роль name у пользователя по id<br>

***Роли в AspNet Identity***<br>

Каждая роль в ASP.NET Identity представляет объект интерфейса IRole. В Entity Framework имеется встроенная реализация данного интерфейса в виде класса IdentityRole.<br>
Класс IdentityRole имеет следующие свойства<br>

Id: уникальный идентификатор роли<br>
- Name: название роли<br>
- Users: коллекция объектов IdentityUserRole, который связывают пользователя и роль<br>

Для управления ролями в ASP.NET Identity имеется класс RoleManager<T>, где T представляет реализацию интерфейса IRole.<br> 
Класс RoleManager управляет ролями с помощью следующих синхронных и асинхронных методов:<br>

-   Create(role) / CreateAsync(role): создает новую роль с именем role<br>
-   Delete(role) / DeleteAsync(role): удаляет роль с именем role<br>
-   FindById(id) / FindByIdAsync(id): ищет роль по id<br>
-   FindByName(name) / FindByNameAsync(name): ищет роль по названию<br>
-   RoleExists(name) / RoleExistsAsync(name): возвращает true, если роль с данным названием существует<br>
-   Update(role) / UpdateAsync(role): обновляет роль<br>
-   Roles: возвращает набор всех имеющихся ролей<br>

**Авторизация**
Работа с аутентификаций для этого мы использовали Controller **AccountController** cсылка-[link AccountController.cs](./yogaAshram/Controllers/AccountController.cs).<br> 
В **AccountController** есть методы ***Register, Login, Logout*** методы работают через Identity.<br>
Каждый метод работает по своему назначению как на наименованиях. В системе сразу нет регистраций потому что есть Role Chief считается как супер админ который уже внедрен в систему. cсылка-[link RoleInitializer.cs](./yogaAshram/Services/RoleInitializer.cs)<br>
 
***User Roles*** 
-   Chief
-   Manager
-   Admin
-   Seller
-   Marketer

***Логика внеренного супер админа***<br>

Chief инициализирован изначально и создает всех пользователей с ролями<br>
При создании пользователя, генерируется  одноразовый пароль  и отправляется на его электронную почту cсылка-[link EmailServices.cs](./yogaAshram/Services/EmailServices.cs)<br>
Пользователь заходит с одноразовым паролем и меняет одноразовый пароль на постоянный<br>


***Восстановление пароля****

Восстановление пароля через почту, нужно будет вести почту с которым за регистрировали ваш Chief и придеть сообщения на почту. cсылка-[link AccountController.cs](./yogaAshram/Controllers/AccountController.cs) метод ***ForgotPassword***<br>
***Сообщения****<br>
Здраствуйте!<br>
Вы запросили восстановление пароля.<br>
Пожалуйста, перейдите по этой ссылке, придумайте и введите ваш новый пароль.<br>

## Описание БД
Работа с базой данных все таблицы находится у нас в Models папке cсылка-[link Models](./yogaAshram/Models)<br>

**Таблицы :**

- Employee- таблица (Работников)
- Branch- таблица (Филиалов)
- Group- таблица (Группы)
- Clients- таблица (Клиентов)
- Attendance- таблица (Посещаемость занятии)
- AttendanceCount- таблица (Вспомогательная таблица для подсчета  текущего  посещения клиента)
- Membership- таблица (Описывает модель абонемента)
- ClientsMembership- таблица (Для хранения данных о покупке клиента определенного абонемента)
- Scheduele- таблица (Описывает модель расписания группы)
- Payments- таблица (Кассы)


**Таблица Employee- таблица (Работников) поля :** 

-   NameSurname- текстовое поля для И.Ф.О 
-   OnTimePassword- bool значение ложный и верность своевременный пароль 
-   Role текстовое- для определения роли работников

**Branch- таблица (Филиалов)**

-   Id- числовой первичный ключ Филиала
-   Name- текстовое наименования Филиала
-   Address- текстовое адрес Филиала
-   Info- текстовое иформация о Филиале
-   AdminId- числовой вторичный ключ таблицы(Employee)

**Group- таблица (Группов)**

-   Id- числовой первичный ключ Группы   
-   Name- текстовое наименования Филиала
-   BranchId- числовой вторичный ключ таблицы(Branch)
-   CoachId- числовой вторичный ключ таблицы(Employee)
-   CreatorId- числовой вторичный ключ таблицы(Employee)

**Clients- таблица (Клиентов)**

-   Id- числовой первичный ключ Клиентов  
-   NameSurname- текстовое наименования Ф.И.О
-   DateOfBirth- дата тиме дата рождения клиента 
-   HasMembership- bool верность или ложный 
-   PhoneNumber- текстовое номер телефона клиента
-   Email- текстовое email клиента
-   Address- текстовое адрес клиента
-   WorkPlace- текстовое работа клиента где работает 
-   ClientType- список типа о здоровий клиента боллейт ли он 
-   LessonNumbers- числовой Номера уроков
-   Color- текстовое для определения цвета клиента
-   Source- текстовое источник откуда нас узнали клиенты
-   GroupId- числовой вторичный ключ таблицы(Group)
-   CreatorId- числовой вторичный ключ таблицы(Employee)
-   Comments- список комментарий
-   Balance- числовой сумма баланса
-   Paid- bool ложный или верность оплачено или нет 
-   Contract- bool ложный или верность есть ли Договор 
-   WhatsAppGroup- bool ложный или верность состойт в группе WhatsAppGroup
-   DateCreate- дата времений создания клиента
-   MembershipId- числовой вторичный ключ таблицы(Membership)
-   SicknessId- числовой вторичный ключ таблицы(Sickness)

**Attendance- таблица (Посещаемость занятии)**

-   Id- числовой первичный ключ Посещаемость занятии 
-   ClientId- числовой вторичный ключ таблицы(Client)
-   GroupId- числовой вторичный ключ таблицы(Group)
-   MembershipId- числовой вторичный ключ таблицы(Membership)
-   ClientsMembershipId- числовой вторичный ключ таблицы(ClientsMembership)
-   IsChecked- bool значение ложный и верность Проверено
-   IsNotActive- bool значение ложный и верность не активный клиент
-   Date- значение даты при отметки клиентов 
-   AttendanceCountId- числовой вторичный ключ таблицы(AttendanceCount)

**AttendanceCount- таблица (Вспомогательная таблица для подсчета  текущего  посещения клиента)**

-   Id- числовой первичный ключ подсчета  текущего  посещения клиента
-   AttendingTimes- числовой наподчет  Время посещения
-   AbsenceTimes- числовой на подчет Время отсутствия
-   FrozenTimes- числовой на подчет Замороженные времена

**Membership- таблица (Описывает модель абонемента)**

-   Id- числовой первичный ключ описывает модель абонемента
-   Name- текстовое наименования модель абонемента
-   Price- числовой цены абонемента- текстовое наименования
-   AttendanceDays- числовой вторичный ключ таблицы(Group)
-   CategoryId- числовой вторичный ключ таблицы(Category)
      
**ClientsMembership- таблица (Для хранения данных о покупке клиента определенного абонемента)**

-   Id- числовой первичный ключ хранения данных о покупке клиента определенного абонемента
-   DateOfPurchase- дата времений  Дата покупки абонемента
-   DateOfExpiry- дата времений дата истечения срока абонемента
-   FirstDateOfLesson- дата времений первое посещения урока абонемента
-   ClientId- числовой вторичный ключ таблицы(Client) 
-   MembershipId- числовой вторичный ключ таблицы(Membership)

**Scheduele- таблица (Описывает модель расписания группы)**

-   Id- числовой первичный ключ модель расписания группы
-   BranchId- числовой вторичный ключ таблицы(Branch)
-   DayOfWeeksString- список дня недели строки
-   StartTime- дата времений начало занятий
-   FinishTime- дата времений конец занятий
-   GroupId- числовой вторичный ключ таблицы(Group)
-   Groups- список Группы
-   ChosenDate- дата выбранная дата занятии группы 
-   Attendances- список посещаемости
-   NameSurname- текстовое наименования Ф.И.О
-   PhoneNumber- текстовое ведения номера 

**Payments- таблица (Кассы)**

-   long Id- числовой первичный ключ кассы
-   Comment- текстовое для описания комментарий 
-   CreatorId- числовой вторичный ключ таблицы(Employee)
-   ClientsMembershipId- числовой вторичный ключ таблицы(ClientsMembership)
-   Debts- числовой сумма долгов  
-   CashSum- числовой сумма наличий 
-   CardSum- числовой сумма баковской карты  
-   TotalSum- числовой для вывов тотальной суммы 
-   CateringDate- Дата времений кейтерий Дата
-   LastUpdate- Дата времений последнее обновление
-   Type- типывое для типа кассы  
-   BranchId- числовой вторичный ключ таблицы(Branch)



## Описание API#

Мы использовали принцип code first, т.е проектировали сначала классы и связи между ними и в бд по этим классам создавались соответствующие таблицы<br>

**Использованные технологии и библиотеки**<br>

- ASP.NET Core Identity 
- Bootstrap
- Chart Js
- JQuery
- Telegram.Bot
- PDFSharpCore
- Quartz.Net

**Валидация ValidationController**

Где хранится cсылка-[link ValidationController.cs](./yogaAshram/Controllers/ValidationController.cs)<br>
Где хранится cсылка-[link ModelsView.cs](./yogaAshram/Models/ModelsView)<br>

- CheckEditUserName - метод рассматрисвает на редактирования UserName и смотрить на совпадения 
- CheckEditEmail - метод рассматрисвает на редактирования Email и смотрить на совпадения 

***И.Т.Д все почти меттоды в ValidationController смотрят на совпаления личных данных и сообщает пользовалю***


**Платежная система методы**<br>

Где хрантся cсылка-[link PaymentsController.cs](./yogaAshram/Controllers/PaymentsController.cs)<br>

- EditAjax - метод редактирования через ajax
- GetEditModalAjax - получение модального окна  с формой для редактирования
- CreateAjax - создание через ajax
- GetClientMembership - получение экземпляра ClientMembership
- GetPaymentsChartAjax - получение графика платежей
- GetNewPaymentsChartAjax - получение графика платежей от новых клиентов
- GetNewClientsChartAjax - получение графика прибавления клиентов ежемесячно
- SetBarChartQuery - формирование списка данных для столбчатого графика в зависимости от кол-ва данных. Может быть по дням, по неделям и по месяцам
- SetColumnsByWeek - формирует список данных по неделям для ст. графика
- GetPaymentsBarChartAjax - получение ст. графика платежей
- SetChartParams - формирует компоненты графика
- GetMaxYValue - получение максимального значения по оси У
- GetAxisYStep - получение промежутка между шагами по оси У
- GetCreateModalAjax - получение мадального окна создания платежа
- Index - возвращает список платежей
- GetFilteredByDate - фильтрует платежи по дате
- SortPayments - сортирует платежи по выбранной выборке

**Использование посторонних библиотек**

Где хрантся cсылка-[link Services.cs](./yogaAshram/Services)<br>

**генерируется пдф договор**
cсылка-[link PDFSharpCore](./yogaAshram/Services/ContractPdfServices.cs)<br>

- RenderPdfDocument - генерируется PDF договор каждому клиенту. Скачивается договор в Laptop.

**Email регистрация**
cсылка-[link Email](./yogaAshram/Services/EmailServices.cs)<br>
- SendMessageAsync - отправляет на почту пароль пользователя с сообщенем что за регистрировались.

**Telegram Bot**
cсылка-[link Telegram Bot](./yogaAshram/Services/TelegramBot)<br>

- StartBot - запуск бота о на поменания Админу что есть не отмеченные клиенты.


## Предварительная настройка приложения для запуска

Запустится локальный сервер.<br>
Нужно будет открыть в браузере https://localhost:5001/ или http://localhost:5000/.<br>
Так как это система для бизнес логики студии ***Classic Yoga Ashram***. Первым зпускается Авторизация в самом приложение, и в приложений есть уже готовый 
супер админ с ролем Chief. Chief логируется в приложения дальше сразу должен создать работников. И так после создания работников создается Администратару 
Филиалы после создания филиалов нужно каждому филиалу создать группы у группы, есть клинты также создаются клиенты.<br> 
Дальше у Админастраций сайта, есть клинты . Клиенты могут купить абонименты у абониментов есть, дата и сумма. После приобритения Абонимента клиент полноценно 
может заниматься в группе . Смортя на Абонимент есть заниятия посвящения занятия, заниятий могут отмечать, заморозать и так же отменять. После заморозки 
или же отмены занятий переходить на следующий день.<br>

 
