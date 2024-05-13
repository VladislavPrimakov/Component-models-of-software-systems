package app.service;

import app.entity.User;
import jakarta.persistence.EntityManager;

import java.util.Optional;

public class UserService {

    private final EntityManager em;

    public UserService(EntityManager entityManager) {
        this.em = entityManager;
    }

    public Optional<User> auth(String name, String password) {
        em.getTransaction().begin();

        Optional<User> res = em.createQuery("SELECT u from User u where name = :username and password = :pass", User.class)
                .setParameter("username", name)
                .setParameter("pass", password)
                .getResultStream()
                .findFirst();

        em.getTransaction().commit();
        return res;
    }

}
