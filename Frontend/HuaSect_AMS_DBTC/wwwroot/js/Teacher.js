/**
 * Teacher Dashboard — Teacher.js
 * Handles: sidebar toggle, mobile menu, live course search
 */

(function () {
    "use strict";

    /* ── DOM refs ── */
    const sidebar = document.getElementById("sidebar");
    const sidebarToggle = document.getElementById("sidebarToggle");
    const mobileMenuBtn = document.getElementById("mobileMenuBtn");
    const sidebarOverlay = document.getElementById("sidebarOverlay");
    const mainWrapper = document.querySelector(".main-wrapper");

    /* ─────────────────────────────────────────
       SIDEBAR COLLAPSE (desktop)
    ───────────────────────────────────────── */
    const COLLAPSED_KEY = "sidebar_collapsed";

    function applySidebarState(collapsed) {
        if (collapsed) {
            sidebar.classList.add("collapsed");
        } else {
            sidebar.classList.remove("collapsed");
        }
        // Keep main-wrapper margin in sync when JS-managed
        if (mainWrapper) {
            mainWrapper.style.marginLeft = collapsed
                ? "var(--sidebar-collapsed)"
                : "var(--sidebar-w)";
        }
        sessionStorage.setItem(COLLAPSED_KEY, collapsed ? "1" : "0");
    }

    if (sidebarToggle) {
        // Restore persisted state
        const wasCollapsed = sessionStorage.getItem(COLLAPSED_KEY) === "1";
        applySidebarState(wasCollapsed);

        sidebarToggle.addEventListener("click", () => {
            const isCollapsed = sidebar.classList.contains("collapsed");
            applySidebarState(!isCollapsed);
        });
    }

    /* ─────────────────────────────────────────
       MOBILE SIDEBAR
    ───────────────────────────────────────── */
    function openMobileSidebar() {
        sidebar.classList.add("mobile-open");
        sidebarOverlay.classList.add("visible");
        document.body.style.overflow = "hidden";
    }

    function closeMobileSidebar() {
        sidebar.classList.remove("mobile-open");
        sidebarOverlay.classList.remove("visible");
        document.body.style.overflow = "";
    }

    if (mobileMenuBtn) {
        mobileMenuBtn.addEventListener("click", openMobileSidebar);
    }

    if (sidebarOverlay) {
        sidebarOverlay.addEventListener("click", closeMobileSidebar);
    }

    // Close on nav-item click (mobile)
    document.querySelectorAll(".nav-item").forEach((item) => {
        item.addEventListener("click", () => {
            if (window.innerWidth <= 768) closeMobileSidebar();
        });
    });

    // Reset on resize
    window.addEventListener("resize", () => {
        if (window.innerWidth > 768) closeMobileSidebar();
    });

    /* ─────────────────────────────────────────
       LIVE COURSE SEARCH
    ───────────────────────────────────────── */
    const searchInput = document.getElementById("courseSearch");
    const courseCards = document.querySelectorAll(".course-card");
    const emptyState = document.getElementById("emptyState");

    if (searchInput && courseCards.length) {
        searchInput.addEventListener("input", function () {
            const q = this.value.trim().toLowerCase();
            let visible = 0;

            courseCards.forEach((card) => {
                const title = (card.dataset.title || "").toLowerCase();
                const code = (card.dataset.code || "").toLowerCase();
                const faculty = (card.dataset.faculty || "").toLowerCase();

                const match = !q || title.includes(q) || code.includes(q) || faculty.includes(q);

                if (match) {
                    card.style.display = "";
                    visible++;
                } else {
                    card.style.display = "none";
                }
            });

            if (emptyState) {
                emptyState.style.display = visible === 0 ? "block" : "none";
            }
        });
    }

    /* ─────────────────────────────────────────
       CARD ENTRANCE ANIMATION STAGGER
       (re-triggers after search clears)
    ───────────────────────────────────────── */
    function restaggerCards() {
        const visible = Array.from(courseCards).filter(c => c.style.display !== "none");
        visible.forEach((card, i) => {
            card.style.animationDelay = `${i * 0.07}s`;
            card.classList.remove("animated");
            void card.offsetWidth; // reflow
            card.classList.add("animated");
        });
    }

    /* ─────────────────────────────────────────
       TOOLTIP ACCESSIBILITY (collapsed mode)
    ───────────────────────────────────────── */
    document.querySelectorAll(".nav-item[data-tooltip]").forEach((item) => {
        item.setAttribute("aria-label", item.dataset.tooltip);
    });

    /* ─────────────────────────────────────────
       ATTENDANCE BUTTON FEEDBACK
    ───────────────────────────────────────── */
    document.querySelectorAll(".btn-primary[data-href]").forEach((btn) => {
        btn.addEventListener("click", function () {
            window.location.href = this.dataset.href;
        });
    });

    /* ─────────────────────────────────────────
       KEYBOARD NAVIGATION FOR SIDEBAR TOGGLE
    ───────────────────────────────────────── */
    if (sidebarToggle) {
        sidebarToggle.addEventListener("keydown", (e) => {
            if (e.key === "Enter" || e.key === " ") {
                e.preventDefault();
                sidebarToggle.click();
            }
        });
    }

})();