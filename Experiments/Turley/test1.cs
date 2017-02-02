using System;
namespace test1{

    public static final int TECH_DEFAULT = 0;
    public static final int AGE_DEFAULT = 55;
    public static final String NAME_DEFAULT = "Professor";
    public static final int SUBJECT_DEFAULT = 0;

    class professor{
        int tech;
        int age;
        String name;
        int subject;

        public professor(){
            tech = TECH_DEFAULT;
            age = AGE_DEFAULT;
            name = NAME_DEFAULT
            subject = SUBJECT_DEFAULT;
        }

        public int getAge(){
            return this.age;
        }

        public void rename(String new){
            this.name = new;
        }

    }
}