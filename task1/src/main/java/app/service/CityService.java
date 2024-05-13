package app.service;

import app.entity.City;
import jakarta.persistence.EntityManager;

import java.util.List;

public class CityService {
    private final EntityManager em;

    public CityService(EntityManager entityManager) {
        this.em = entityManager;
    }

    public List<City> GetAllCities(){
        em.getTransaction().begin();
        List<City> cities =  em.createQuery("SELECT c FROM City c", City.class).getResultList();
        em.getTransaction().commit();
        return cities;
    }
}
