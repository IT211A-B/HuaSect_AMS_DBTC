/**
 * Courses.js — Attendance Management Dashboard
 * Handles: combo box, date picker, attendance toggling, save, filtering
 */

(function () {
    'use strict';

    /* ─── State ─────────────────────────────────────────── */
    const state = {
        selectedCourse: null,
        selectedDate: null,
        attendance: {},          // { studentId: 'present' | 'late' | 'absent' }
        filteredStudents: [],
    };

    /* ─── DOM references ────────────────────────────────── */
    const courseSelect = document.getElementById('courseCombo');
    const courseSearch = document.getElementById('courseSearchBox');
    const courseOptions = document.getElementById('courseOptions');
    const searchInput = document.getElementById('courseSearchInput');
    const datePicker = document.getElementById('datePicker');
    const tableBody = document.getElementById('studentTableBody');
    const saveBtn = document.getElementById('saveBtn');
    const backBtn = document.getElementById('backBtn');
    const toast = document.getElementById('toast');

    /* ─── Combo Box ─────────────────────────────────────── */
    function initComboBox() {
        if (!courseSelect) return;

        courseSelect.addEventListener('click', function (e) {
            e.stopPropagation();
            const isOpen = courseSearch.classList.toggle('open');
            if (isOpen && searchInput) {
                searchInput.value = '';
                renderOptions('');
                searchInput.focus();
            }
        });

        if (searchInput) {
            searchInput.addEventListener('input', function () {
                renderOptions(this.value.trim().toLowerCase());
            });

            searchInput.addEventListener('keydown', function (e) {
                if (e.key === 'Escape') closeCombo();
            });
        }

        document.addEventListener('click', function (e) {
            if (!courseSearch.contains(e.target) && e.target !== courseSelect) {
                closeCombo();
            }
        });
    }

    function renderOptions(filter) {
        if (!courseOptions) return;
        const options = Array.from(courseSelect.options).slice(1); // skip placeholder
        courseOptions.innerHTML = '';

        const matched = options.filter(opt =>
            !filter || opt.text.toLowerCase().includes(filter)
        );

        if (matched.length === 0) {
            courseOptions.innerHTML = '<div class="combo-option" style="color:var(--gray-mid);cursor:default;">No results</div>';
            return;
        }

        matched.forEach(opt => {
            const el = document.createElement('div');
            el.className = 'combo-option';
            el.textContent = opt.text;
            el.dataset.value = opt.value;

            if (opt.value === state.selectedCourse) {
                el.classList.add('highlighted');
            }

            el.addEventListener('click', function () {
                selectCourse(this.dataset.value, opt.text);
                closeCombo();
            });

            courseOptions.appendChild(el);
        });
    }

    function selectCourse(value, text) {
        state.selectedCourse = value;
        if (courseSelect) courseSelect.value = value;
        filterStudents();
        updateCourseLabel(text);
    }

    function updateCourseLabel(text) {
        if (courseSelect) {
            const placeholderOpt = courseSelect.querySelector('option[value=""]');
            if (!placeholderOpt || courseSelect.value) {
                // Already selected via native select
            }
        }
    }

    function closeCombo() {
        if (courseSearch) courseSearch.classList.remove('open');
    }

    /* Native select fallback */
    function initNativeSelect() {
        if (!courseSelect) return;
        courseSelect.addEventListener('change', function () {
            state.selectedCourse = this.value;
            filterStudents();
        });
    }

    /* ─── Date Picker ───────────────────────────────────── */
    function initDatePicker() {
        if (!datePicker) return;

        // Set default to today
        const today = new Date();
        datePicker.value = formatDateInput(today);
        state.selectedDate = datePicker.value;

        datePicker.addEventListener('change', function () {
            state.selectedDate = this.value;
        });
    }

    function formatDateInput(date) {
        const y = date.getFullYear();
        const m = String(date.getMonth() + 1).padStart(2, '0');
        const d = String(date.getDate()).padStart(2, '0');
        return `${y}-${m}-${d}`;
    }

    /* ─── Student Filtering ─────────────────────────────── */
    function filterStudents() {
        const allRows = tableBody
            ? Array.from(tableBody.querySelectorAll('.student-row[data-course]'))
            : [];

        allRows.forEach(row => {
            const course = row.dataset.course;
            const show = !state.selectedCourse || course === state.selectedCourse;
            row.style.display = show ? '' : 'none';
        });

        // Show empty state if no visible rows
        const visible = allRows.filter(r => r.style.display !== 'none');
        toggleEmptyState(visible.length === 0);
    }

    function toggleEmptyState(show) {
        let emptyEl = document.getElementById('emptyState');
        if (show && !emptyEl) {
            emptyEl = document.createElement('div');
            emptyEl.id = 'emptyState';
            emptyEl.className = 'empty-state';
            emptyEl.innerHTML = `
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8zm-1-13h2v6h-2zm0 8h2v2h-2z"/>
                </svg>
                No students found for the selected course.
            `;
            if (tableBody) tableBody.appendChild(emptyEl);
        } else if (!show && emptyEl) {
            emptyEl.remove();
        }
    }

    /* ─── Attendance Toggle ─────────────────────────────── */
    function initAttendanceButtons() {
        if (!tableBody) return;

        tableBody.addEventListener('click', function (e) {
            const btn = e.target.closest('.att-btn');
            if (!btn) return;

            const row = btn.closest('.student-row');
            if (!row) return;

            const studentId = row.dataset.studentId;
            const type = btn.dataset.type; // 'present' | 'late' | 'absent'

            // Deactivate siblings
            const siblings = row.querySelectorAll('.att-btn');
            siblings.forEach(b => b.classList.remove('active'));

            // Toggle current
            const wasActive = state.attendance[studentId] === type;
            if (wasActive) {
                delete state.attendance[studentId];
            } else {
                btn.classList.add('active');
                state.attendance[studentId] = type;
            }
        });
    }

    /* ─── Save ──────────────────────────────────────────── */
    function initSaveButton() {
        if (!saveBtn) return;

        saveBtn.addEventListener('click', function () {
            const payload = {
                course: state.selectedCourse,
                date: state.selectedDate,
                attendance: state.attendance,
            };

            console.log('[Courses] Saving attendance:', payload);

            // Simulate async save
            saveBtn.disabled = true;
            saveBtn.textContent = 'Saving…';

            setTimeout(function () {
                saveBtn.disabled = false;
                saveBtn.textContent = 'Save';
                showToast('Attendance saved successfully!');

                // POST to controller
                submitAttendance(payload);
            }, 800);
        });
    }

    async function submitAttendance(payload) {
        try {
            const resp = await fetch('/Courses/SaveAttendance', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': getAntiForgeryToken(),
                },
                body: JSON.stringify(payload),
            });

            if (!resp.ok) {
                console.error('[Courses] Save failed:', resp.status);
            }
        } catch (err) {
            console.warn('[Courses] Network error (likely dev mode):', err.message);
        }
    }

    function getAntiForgeryToken() {
        const el = document.querySelector('input[name="__RequestVerificationToken"]');
        return el ? el.value : '';
    }

    /* ─── Back Button ───────────────────────────────────── */
    function initBackButton() {
        if (!backBtn) return;
        backBtn.addEventListener('click', function () {
            if (window.history.length > 1) {
                window.history.back();
            } else {
                window.location.href = '/';
            }
        });
    }

    /* ─── Toast ─────────────────────────────────────────── */
    function showToast(message, duration) {
        if (!toast) return;
        toast.textContent = message;
        toast.classList.add('show');

        setTimeout(function () {
            toast.classList.remove('show');
        }, duration || 2800);
    }

    /* ─── Sidebar Active State ──────────────────────────── */
    function initSidebarNav() {
        const navIcons = document.querySelectorAll('.nav-icon');
        navIcons.forEach(icon => {
            icon.addEventListener('click', function (e) {
                e.preventDefault();
                navIcons.forEach(i => i.classList.remove('active'));
                this.classList.add('active');
            });
        });
    }

    /* ─── Row Animation ─────────────────────────────────── */
    function animateRows() {
        const rows = document.querySelectorAll('.student-row');
        rows.forEach((row, i) => {
            row.style.opacity = '0';
            row.style.transform = 'translateY(8px)';
            row.style.transition = `opacity 0.3s ${i * 0.06}s ease, transform 0.3s ${i * 0.06}s ease`;

            // Trigger reflow
            requestAnimationFrame(() => {
                requestAnimationFrame(() => {
                    row.style.opacity = '';
                    row.style.transform = '';
                });
            });
        });
    }

    /* ─── Init ──────────────────────────────────────────── */
    function init() {
        initComboBox();
        initNativeSelect();
        initDatePicker();
        initAttendanceButtons();
        initSaveButton();
        initBackButton();
        initSidebarNav();
        animateRows();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();