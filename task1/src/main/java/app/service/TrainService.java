package app.service;

import app.entity.City;
import app.entity.Ticket;
import app.entity.Train;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;

import java.time.Instant;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class TrainService {
    private final EntityManager em;

    public TrainService(EntityManager entityManager) {
        this.em = entityManager;
    }

    public boolean addTrain(LocalDateTime startTime, LocalDateTime finishTime, int startCityID, int finishCityID, int numTickets) {
        em.getTransaction().begin();
        Optional<City> startCity = Optional.ofNullable(em.find(City.class, startCityID));
        Optional<City> finishCity = Optional.ofNullable(em.find(City.class, finishCityID));
        if (startCity.isPresent() && finishCity.isPresent()) {
            Train train = new Train(startTime, finishTime, startCity.get(), finishCity.get());
            em.persist(train);
            for (int i = 0; i < numTickets; i++) {
                Ticket ticket = new Ticket(i + 1, train);
                em.persist(ticket);
            }
            em.getTransaction().commit();
            return true;
        }
        em.getTransaction().commit();
        return false;
    }

    public List<Train> getAllTrains() {
        em.getTransaction().begin();
        List<Train> res = em.createQuery("SELECT t FROM Train t join t.tickets ts", Train.class).getResultList();
        em.getTransaction().commit();
        return res;
    }

    public List<Train> getAllTrains(LocalDate date, int startCityID, int finishCityID) {
        if (date != null) {
            return getAllTrains(date.atStartOfDay(), date.atTime(LocalTime.MAX), startCityID, finishCityID);
        }
        return getAllTrains(null, null, startCityID, finishCityID);
    }

    public List<Train> getAllTrains(LocalDateTime startDateTime, LocalDateTime finishDateTime, int startCityID, int finishCityID) {
        String conditions = "";
        if (startDateTime != null) {
            conditions += " AND t.startTime >= :startDateStartDayTime";
        }
        if (startDateTime != null) {
            conditions += " AND t.startTime <= :startDateFinishDayTime";
        }
        if (startCityID > 0) {
            conditions += " AND t.startCity.id = :startCityID";
        }
        if (finishCityID > 0) {
            conditions += " AND t.finishCity.id = :finishCityID";
        }
        conditions = "Where tc.status = false " + conditions;

        em.getTransaction().begin();

        TypedQuery<Train> query = em.createQuery("SELECT t FROM Train t join t.tickets tc " + conditions + " order by t.startTime, tc.number", Train.class);
        if (startDateTime != null) {
            query.setParameter("startDateStartDayTime", startDateTime);
        }
        if (startDateTime != null) {
            query.setParameter("startDateFinishDayTime", finishDateTime);
        }
        if (startCityID > 0) {
            query.setParameter("startCityID", startCityID);
        }
        if (finishCityID > 0) {
            query.setParameter("finishCityID", finishCityID);
        }

        List<Train> res = query.getResultList();
        em.getTransaction().commit();
        return res;
    }
}