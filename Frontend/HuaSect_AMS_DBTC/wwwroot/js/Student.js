
(function () {
    'use strict';

    let students = [
        { id: 1, name: 'Maria Santos', roll: '2024-0001', course: 'BS Computer Science', section: '3-A' },
        { id: 2, name: 'Jose Reyes', roll: '2024-0002', course: 'BS Information Technology', section: '2-B' },
        { id: 3, name: 'Anna Cruz', roll: '2024-0003', course: 'BS Nursing', section: '1-C' },
        { id: 4, name: 'Miguel dela Cruz', roll: '2024-0004', course: 'BS Accountancy', section: '4-A' },
        { id: 5, name: 'Sofia Garcia', roll: '2024-0005', course: 'BS Architecture', section: '3-B' },
        { id: 6, name: 'Carlos Mendoza', roll: '2024-0006', course: 'BS Civil Engineering', section: '2-A' },
    ];

    let nextId = 7;
    let pendingDeleteId = null;
    let editingId = null;

    const grid = document.getElementById('studentGrid');
    const emptyState = document.getElementById('emptyState');
    const searchInput = document.getElementById('searchInput');
    const clearBtn = document.getElementById('clearSearch');
    const totalCount = document.getElementById('totalCount');
    const presentCount = document.getElementById('presentCount');

    // Delete modal
    const deleteModal = document.getElementById('deleteModal');
    const confirmDelete = document.getElementById('confirmDelete');
    const cancelDelete = document.getElementById('cancelDelete');

    // Student form modal
    const studentModal = document.getElementById('studentModal');
    const studentModalTitle = document.getElementById('studentModalTitle');
    const saveStudentBtn = document.getElementById('saveStudentBtn');
    const cancelStudentModal = document.getElementById('cancelStudentModal');
    const fieldName = document.getElementById('fieldName');
    const fieldRoll = document.getElementById('fieldRoll');
    const fieldCourse = document.getElementById('fieldCourse');
    const fieldSection = document.getElementById('fieldSection');

    // Open-add triggers
    document.getElementById('addStudentBtn').addEventListener('click', () => openStudentModal());
    document.getElementById('sidebarAddBtn').addEventListener('click', () => openStudentModal());
    document.getElementById('attendanceBtn').addEventListener('click', () => showToast('Attendance session started ✓'));

    document.querySelectorAll('.sidebar__item').forEach(item => {
        item.addEventListener('click', function (e) {
            if (this.id === 'sidebarAddBtn') return;
            document.querySelectorAll('.sidebar__item').forEach(i => i.classList.remove('active'));
            this.classList.add('active');
        });
    });

    searchInput.addEventListener('input', () => {
        const q = searchInput.value.trim();
        clearBtn.classList.toggle('visible', q.length > 0);
        renderCards(filtered());
    });

    clearBtn.addEventListener('click', () => {
        searchInput.value = '';
        clearBtn.classList.remove('visible');
        renderCards(students);
    });

    function filtered() {
        const q = searchInput.value.trim().toLowerCase();
        if (!q) return students;
        return students.filter(s =>
            s.name.toLowerCase().includes(q) ||
            s.roll.toLowerCase().includes(q)
        );
    }

    /* ─── Render ──────────────────────────────────────────────── */
    function renderCards(list) {
        grid.innerHTML = '';
        emptyState.style.display = list.length === 0 ? 'block' : 'none';

        list.forEach((s, i) => {
            const card = document.createElement('div');
            card.className = 'student-card';
            card.style.animationDelay = `${i * 0.05}s`;
            card.dataset.id = s.id;

            card.innerHTML = `
                <div class="card__header">
                    <div class="card__avatar">${initials(s.name)}</div>
                    <div class="card__info">
                        <div class="card__name">${esc(s.name)}</div>
                        <div class="card__roll">${esc(s.roll)}</div>
                    </div>
                </div>
                <div class="card__body">
                    <div class="card__course">
                        <span class="material-icons-round">menu_book</span>
                        <span class="card__course-text">${esc(s.course)} &mdash; Sec ${esc(s.section)}</span>
                    </div>
                    <div class="card__actions">
                        <button class="card__view-btn js-view" data-id="${s.id}">
                            <span class="material-icons-round">bar_chart</span>
                            View Attendance History
                        </button>
                        <div class="card__icon-btns">
                            <button class="card__icon-btn card__icon-btn--edit js-edit" data-id="${s.id}" title="Update">
                                <span class="material-icons-round">edit</span>
                            </button>
                            <button class="card__icon-btn card__icon-btn--delete js-delete" data-id="${s.id}" title="Delete">
                                <span class="material-icons-round">delete_outline</span>
                            </button>
                        </div>
                    </div>
                </div>`;

            grid.appendChild(card);
        });

        updateStats();
        bindCardEvents();
    }

    function bindCardEvents() {
        document.querySelectorAll('.js-delete').forEach(btn => {
            btn.addEventListener('click', () => openDeleteModal(+btn.dataset.id));
        });
        document.querySelectorAll('.js-edit').forEach(btn => {
            btn.addEventListener('click', () => openStudentModal(+btn.dataset.id));
        });
        document.querySelectorAll('.js-view').forEach(btn => {
            btn.addEventListener('click', () => {
                const s = students.find(x => x.id === +btn.dataset.id);
                showToast(`Viewing attendance for ${s.name}`);
            });
        });
    }

    function updateStats() {
        totalCount.textContent = students.length;
        presentCount.textContent = Math.floor(students.length * 0.78);
    }

    function openDeleteModal(id) {
        pendingDeleteId = id;
        deleteModal.classList.add('open');
    }

    function closeDeleteModal() {
        deleteModal.classList.remove('open');
        pendingDeleteId = null;
    }

    confirmDelete.addEventListener('click', () => {
        if (pendingDeleteId === null) return;
        students = students.filter(s => s.id !== pendingDeleteId);
        closeDeleteModal();
        renderCards(filtered());
        showToast('Student record deleted.');
    });

    cancelDelete.addEventListener('click', closeDeleteModal);

    deleteModal.addEventListener('click', e => {
        if (e.target === deleteModal) closeDeleteModal();
    });

    function openStudentModal(id = null) {
        editingId = id;
        if (id) {
            const s = students.find(x => x.id === id);
            studentModalTitle.textContent = 'Update Student';
            fieldName.value = s.name;
            fieldRoll.value = s.roll;
            fieldCourse.value = s.course;
            fieldSection.value = s.section;
        } else {
            studentModalTitle.textContent = 'Add New Student';
            fieldName.value = '';
            fieldRoll.value = '';
            fieldCourse.value = '';
            fieldSection.value = '';
        }
        studentModal.classList.add('open');
        fieldName.focus();
    }

    function closeStudentModal() {
        studentModal.classList.remove('open');
        editingId = null;
    }

    saveStudentBtn.addEventListener('click', () => {
        const name = fieldName.value.trim();
        const roll = fieldRoll.value.trim();
        const course = fieldCourse.value.trim();
        const section = fieldSection.value.trim();

        if (!name || !roll || !course || !section) {
            showToast('Please fill in all fields.'); return;
        }

        if (editingId) {
            const s = students.find(x => x.id === editingId);
            s.name = name; s.roll = roll; s.course = course; s.section = section;
            showToast('Student record updated.');
        } else {
            students.push({ id: nextId++, name, roll, course, section });
            showToast('Student added successfully ✓');
        }

        closeStudentModal();
        renderCards(filtered());
    });

    cancelStudentModal.addEventListener('click', closeStudentModal);

    studentModal.addEventListener('click', e => {
        if (e.target === studentModal) closeStudentModal();
    });

    /* ─── Toast ───────────────────────────────────────────────── */
    let toastTimer;
    const toast = document.getElementById('toast');

    function showToast(msg) {
        clearTimeout(toastTimer);
        toast.textContent = msg;
        toast.classList.add('show');
        toastTimer = setTimeout(() => toast.classList.remove('show'), 3000);
    }

    /* ─── Helpers ─────────────────────────────────────────────── */
    function initials(name) {
        return name.split(' ').slice(0, 2).map(w => w[0]).join('').toUpperCase();
    }

    function esc(str) {
        const d = document.createElement('div');
        d.textContent = str;
        return d.innerHTML;
    }

    /* ─── Init ────────────────────────────────────────────────── */
    renderCards(students);

})();
