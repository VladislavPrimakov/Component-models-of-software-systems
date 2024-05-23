package app.utils;

import app.entity.Placement;
import app.entity.Product;
import app.entity.User;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import org.hibernate.cfg.Configuration;

public class HebirnateUtil {
    private static EntityManagerFactory emf = null;

    static {
        Configuration configuration = new Configuration();
        configuration.addAnnotatedClass(User.class);
        configuration.addAnnotatedClass(Product.class);
        configuration.addAnnotatedClass(Placement.class);
        configuration.configure();
        emf = configuration.buildSessionFactory();
    }

    public static EntityManager getEntityManager() {
        if (emf != null) {
            return emf.createEntityManager();
        }
        return null;
    }

    private HebirnateUtil() {}

}
