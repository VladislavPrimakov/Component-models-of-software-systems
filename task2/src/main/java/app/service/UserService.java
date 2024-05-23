package app.service;

import app.entity.User;
import jakarta.ejb.Lock;
import jakarta.ejb.LockType;
import jakarta.ejb.Singleton;
import jakarta.ejb.Startup;
import jakarta.persistence.EntityManager;
import app.utils.HebirnateUtil;

import java.util.Optional;

@Singleton
@Startup
public class UserService {

    @Lock(LockType.WRITE)
    public Result<Optional<User>> login(Optional<String> name, Optional<String> password) {
        Result<Optional<User>> result = new Result<>();

        if (name.isPresent()) {
            if (name.get().isEmpty()) {
                result.addError("Name cannot be empty");
            }
        } else {
            result.addError("Name is null");
        }
        if (password.isPresent()) {
            if (password.get().isEmpty()) {
                result.addError("Password cannot be empty");
            }
        } else {
            result.addError("Password is null");
        }
        if (result.hasErrors()) {
            return result;
        }
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        Optional<User> data = entityManager.createQuery("SELECT u from User u where username = :username and password = :pass", User.class)
                .setParameter("username", name.get())
                .setParameter("pass", password.get())
                .getResultStream()
                .findFirst();
        if (data.isPresent()) {
            result.setData(data);
        } else {
            result.addError("Invalid username or password");
        }
        entityManager.getTransaction().commit();
        return result;
    }
}
