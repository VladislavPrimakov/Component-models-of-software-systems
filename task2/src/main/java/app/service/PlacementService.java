package app.service;

import app.entity.Placement;
import app.utils.HebirnateUtil;
import jakarta.ejb.Lock;
import jakarta.ejb.LockType;
import jakarta.ejb.Singleton;
import jakarta.ejb.Startup;
import jakarta.persistence.EntityManager;

import java.util.List;

@Singleton
@Startup
public class PlacementService {

    @Lock(LockType.READ)
    public List<Placement> getPlacements() {
        EntityManager entityManager = HebirnateUtil.getEntityManager();
        entityManager.getTransaction().begin();
        List<Placement> placements = entityManager.createQuery("select p from Placement p", Placement.class).getResultList();
        entityManager.getTransaction().commit();
        return placements;
    }
}
