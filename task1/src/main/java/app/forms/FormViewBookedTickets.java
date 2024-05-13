package app.forms;

import app.entity.Ticket;
import app.entity.User;
import app.service.TicketService;
import app.util.HebirnateUtil;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

public class FormViewBookedTickets extends JFrame {
    private JPanel panelMain;
    private JButton updateRequestButton;
    private JPanel panelRequest;
    User user;

    public FormViewBookedTickets(User user) {
        this.user = user;

        setTitle("Main");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);
        panelRequest.setLayout(new BoxLayout(panelRequest, BoxLayout.Y_AXIS));

        setTickets();

        updateRequestButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                setTickets();
            }
        });
    }

    private void setTickets() {
        panelRequest.removeAll();
        TicketService ticketService = new TicketService(HebirnateUtil.getEntityManager());
        List<Ticket> tickets = ticketService.getAllTickets(user.getId());
        tickets.forEach(this::setTicket);
        panelRequest.revalidate();
        panelRequest.repaint();
    }

    private void setTicket(Ticket ticket) {
        JPanel panel = new JPanel();
        panel.setLayout(new FlowLayout(FlowLayout.LEFT, 5, 5));
        panel.add(new Label(ticket.getTrain().getStartCity().getName() + " -> " + ticket.getTrain().getFinishCity().getName()));
        panel.add(new Label(ticket.getTrain().getStartTime().toString()));
        panel.add(new Label(ticket.getTrain().getFinishTime().toString()));
        panel.add(new Label(String.valueOf(ticket.getNumber())));
        JButton btn = new JButton("Cancel ticket " + ticket.getNumber());
        panel.add(btn);
        btn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                cancelTicket(ticket.getId());
            }
        });
        panelRequest.add(panel);
    }

    private void cancelTicket(int id) {
        TicketService ticketService = new TicketService(HebirnateUtil.getEntityManager());
        boolean res = ticketService.deleteUserFromTicket(id);
        if (res) {
            JOptionPane.showMessageDialog(this, "Ticket cancelled");
        } else {
            JOptionPane.showMessageDialog(this, "Ticket could not be cancelled", "Error", JOptionPane.ERROR_MESSAGE);
        }
        setTickets();
    }
}
