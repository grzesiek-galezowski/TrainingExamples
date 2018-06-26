package _08_fixture_management;

class User {
    private final String name;
    private final String surname;
    private final int age;

    User(String name, String surname, int age) {
        this.name = name;
        this.surname = surname;
        this.age = age;
    }

    String getName() {
        return name;
    }

    String getSurname() {
        return surname;
    }

    int getAge() {
        return age;
    }
}
