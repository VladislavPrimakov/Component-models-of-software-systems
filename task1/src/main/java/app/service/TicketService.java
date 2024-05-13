package app.service;

import app.entity.City;
import app.entity.Ticket;
import app.entity.User;
import jakarta.persistence.EntityManager;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class TicketService {
    private final EntityManager em;

    public TicketService(EntityManager entityManager) {
        this.em = entityManager;
    }

    public boolean setUserToTicket(int ticketID, int userID) {
        em.getTransaction().begin();
        Optional<User> user = Optional.ofNullable(em.find(User.class, userID));
        Optional<Ticket> ticket = Optional.ofNullable(em.find(Ticket.class, ticketID));
        if (user.isPresent() && ticket.isPresent()) {
            if (ticket.get().getStatus()) {
                em.getTransaction().commit();
                return false;
            }
            ticket.get().setUser(user.get());
            ticket.get().setStatus(true);
            em.merge(ticket.get());
            em.getTransaction().commit();
            return true;
        } else {
            em.getTransaction().commit();
            return false;
        }
    }

    public boolean deleteUserFromTicket(int ticketID) {
        em.getTransaction().begin();
        Optional<Ticket> ticket = Optional.ofNullable(em.find(Ticket.class, ticketID));
        if (ticket.isPresent()) {
            if (ticket.get().getStatus()) {
                ticket.get().setStatus(false);
                ticket.get().setUser(null);
                em.merge(ticket.get());
                em.getTransaction().commit();
                return true;
            } else {
                em.getTransaction().commit();
                return false;
            }
        }
        em.getTransaction().commit();
        return false;
    }

    public List<Ticket> getAllTickets(int userID) {
        em.getTransaction().begin();
        Optional<User> user = Optional.ofNullable(em.find(User.class, userID));
        List<Ticket> tickets = new ArrayList<>();
        if (user.isPresent()) {
            tickets = em.createQuery("SELECT t FROM Ticket t join t.train tr where t.status = true", Ticket.class).getResultList();
        }
        em.getTransaction().commit();
        return tickets;
    }
}
