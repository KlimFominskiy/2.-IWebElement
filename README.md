IWebElement

Полезные ссылки:
* https://testengineer.ru/xpath-quick-guide/
* https://testengineer.ru/chrome-developer-tools-dlya-testirovshchika/
* https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html
  

* Для проверок используется класс Assert

### Задание №1. Поиск элементов на странице

За основу берется сайт заявки ипотеку (https://ib.psbank.ru/store/products/classic-mortgage-program).

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/6d84ab4f-96c6-43fe-9909-1e5042669dc2)


Необходимо перейти на эту страницу и найти элементы по XPATH с помощью функции FindElement драйвера IWebDriver:
 * Объект ипотеки 

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/c0451a80-f0cd-45d0-88e1-51bd6f597b5f)

 * Кнопка заполнить через госуслуги
 
![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/8f65ebbe-1428-422e-a4f2-6c423c7ccb0e)

 * Карточка с брендом "Семейная ипотека" 

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/bd589bcb-ee2c-4bb9-88f8-d867216c79b7)

 * Свитчер "Страхование жизни"

 ![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/303fa32c-fc3b-43fe-8227-b4e801200fc9)

 * Поле "Срок кредита" 

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/3b5ffa28-7ce2-4752-b437-233a3314d932)


### Задание №2. Атрибуты элементов

Для элементов описанных в задании 1 получить:
  * Сотояние активности
  * Видимость на странице
  * Для "объекта ипотеки" и поля "срок кредита" - получить установленное значение
  * Для свитчера и карточки - состояние (включен/ выключен)

### Задание №3. Стили элементов

1. Перейти на страницу https://ib.psbank.ru/store/products/classic-mortgage-program
2. Проверить что кнопка "Заполнить через Госуслуги" кликабельна
3. Проверить что цвет кнопки "Заполнить через Госуслуги" равен "#f26126"
4. Проверить что стиль "height" кнопки "Заполнить через Госуслуги" равен "48px"


### Задание №4. Ожидания элементов. Кейс 1

Для работы с данным разделом нужна библиотека Nuget Selenium.Support

1. Перейти на страницу https://ib.psbank.ru/store/products/military-family-mortgage-program
2. Подождать пока пропадет спиннер (лоадер)

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/12126fd1-23e3-4d60-8b7e-e777d2403227)

3. Нажать на кнопку "Заполнить без Госуслуг"
4. Проверить что появилась ошибка

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/4cd9db62-3925-4ef9-9a04-88538b27ed60)

5. Проверить что кнопка "Заполнить без госуслуг" отсутствует на странице
6. Проверить что в течении 10 секунд после нажатия, кнопка "Заполнить без Госуслуг" появилась, а ошибка исчезла
   
### Задание №5. Ожидания элементов. Кейс 2

За основу берется сайт заявки ипотеку (https://ib.psbank.ru/store/products/classic-mortgage-program). 
Для работы с данным разделом нужна библиотека Nuget Selenium.Support

1. Перейти на страницу https://ib.psbank.ru/store/products/classic-mortgage-program
2. Выключить все свитчеры в разделе "Опции, снижающие ставку"

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/43767a5a-393f-4e13-8540-66c2645aed70)

3. Проверить что все свитчеры выключены (Для ожидания выключения используйте класс Wait)
4. Проверить, что дельты процентов, указаные справа серые
5. Поочередно включить свитчеры, проверяя что дельты окрашиваются в зеленый цвет (Для ожидания окраски элемента воспользуйтесь классом Wait)

### Задание №6. Действия с элементами

За основу берется сайт заявки ипотеку (https://ib.psbank.ru/store/products/classic-mortgage-program). 
1. Перейти на страницу https://ib.psbank.ru/store/products/classic-mortgage-program
2. Нажать на кнопку "Заполнить без Госуслуг"
3. Перешли на страницу "Оформить заявку"

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/8ddda90d-7181-4e9a-a1f9-84ca37b1f4fd)

4. Проверить что кнопка "Продолжить" заблокирована
5. Заполнить все поля страницы любыми значениями (ФИО - текст, выбрать пол с помощью нажатия, телефон в формате +7 9xx xxx xx xx, почта в формате xxxxx@xxx.xx, для остальных любые существующие значения)
7. Проверить что кнопка "Продолжить" активна

### Задание №6. Actions. Часть 1

За основу берется сайт заявки ипотеку (https://ib.psbank.ru/store/products/classic-mortgage-program). 
Для работы с данным разделом нужна библиотека Nuget Selenium.Support
1. Перейти на страницу https://ib.psbank.ru/store/products/classic-mortgage-program
2. Проверить что кнопка "Заполнить через Госуслуги" кликабельна
3. Проверить что цвет кнопки "Заполнить через Госуслуги" равен "#f26126"
4. Навестись на кнопку курсорос с помощью класса Actions
5.  Проверить что цвет кнопки "Заполнить через Госуслуги" равен "#b83a12"

### Задание №6. Actions. Часть 2

За основу берется сайт заявки ипотеку (https://ib.psbank.ru/store/products/classic-mortgage-program). 
Для работы с данным разделом нужна библиотека Nuget Selenium.Support
1. Перейти на страницу https://ib.psbank.ru/store/products/classic-mortgage-program
2. Найти слайдер (ползунок) поля "Стоимость недвижимости"

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/0fd0153f-6b93-4fc4-91a8-8a975bdd5ac3)

3. Переместить ползунок с помощью класса Actions

![изображение](https://github.com/AutomationC/3.-IWebElement/assets/22545947/83ff3fc7-334c-4f39-809b-0ff6252e5c43)

4. проверить что значение поля "Стоимость недвижимости" изменилось

