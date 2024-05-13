package app.util;

import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;

public class HebirnateUtil {
    public static EntityManagerFactory emf = null;

    public static EntityManager getEntityManager() {
        if (emf != null) {
            return emf.createEntityManager();
        }
        return null;
    }

    public static void closeEntityManagerFactory() {
        if (emf != null) {
            emf.close();
        }
    }
}
